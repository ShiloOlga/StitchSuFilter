using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.Data.Entities;
using Web.Models;
using FabricItem = Web.Data.Entities.FabricItem;
using Kit = Web.Data.Entities.Kit;

namespace Web.Data.Context
{
    public class DataSeed
    {
        private readonly MariaDbContext _dbContext;
        private readonly ILazyLoader _lazyLoader;
        private const string FileName = "backup.json";

        #region Private classes

        private class F
        {
            public string Type { get; }
            public sbyte Count { get; }
            public string Name { get; }
            public sbyte Priority { get; }
            public string Content { get; }

            public F(string name, sbyte count, string type, sbyte priority, string content)
            {
                Type = type;
                Count = count;
                Name = name;
                Priority = priority;
                Content = content;
            }
        }

        private class SeedKit
        {
            public string Manufacturer { get; set; }
            public string Title { get; set; }
            public string Item { get; set; }
            public string ThreadManufacturer { get; set; }
            public string FabricItem { get; set; }
            public int ColorsCount { get; set; }
            public decimal WidthSm { get; set; }
            public decimal HeightSm { get; set; }
            public short WidthStitches { get; set; }
            public short HeightStitches { get; set; }
            public string Author { get; set; }
            public string Image { get; set; }
            public string Link { get; set; }
        }

        #endregion
        #region Data
        private const string _dmcColors = @"Ecru,Ecru/off-white,#fff7e7
                                    315, Antique Mauve - MED DK,#7d4246
                                    Blanc, White,#eeeeee
                                    316, Antique Mauve - MED,#bc757f
                                    B5200,Snow White,#fcfcff
                                    317, Pewter Gray,#6d6469
                                    White,White,#ffffff
                                    318,Steel Gray - LT,#999b9d
                                    150,Red - BRIGHT,#cf0053
                                    319,Pistachio Green - VY DK,#3a553b
                                    151, Pink,#ffcbd7
                                    320, Pistachio Green - MED,#608c59
                                    152,Tawny - DK,#e1a1a1
                                    321,Red,#bd1136
                                    153,Lilac,#eac5eb
                                    322,Baby Blue,#3a609d
                                    154, Red - VY DK,#4b233a
                                    326, Rose - VY DK,#ac1c37
                                    155, Forget-me-not Blue,#9774b6
                                    327, Violet,#5e0f77
                                    156, Blue - MED,#8577b4
                                    333,Blue Violet - VY DK,#6e2e9b
                                    157, Blue - LT,#b5b8ea
                                    334,Baby Blue - MED,#6085b8
                                    158,Blue - DK,#393068
                                    335,Rose,#d63d57
                                    159,Petrol Blue - LT,#bcb5de
                                    336,Blue,#0c275e
                                    160,Petrol Blue - MED,#8178a9
                                    340,Blue Violet - MED,#996dc3
                                    161,Petrol Blue - DK,#60568b
                                    341,Blue Violet - LT,#a39ad7
                                    162,Baby Blue - LT,#cae7f0
                                    347,Salmon - VY DK,#ab1b33
                                    163, Green,#557a60
                                    349, Coral - DK,#c62c38
                                    164,Green - LT,#bae4b6
                                    350,Coral - MED,#de3f40
                                    165,Green - BRIGHT,#e1f477
                                    351,Coral,#ed625b
                                    166,Lime Green,#adc238
                                    352, Coral - LT,#f78372
                                    167,Khaki Brown,#855d31
                                    353, Peach,#fdb4a1
                                    168, Silver Gray,#b1aeb7
                                    355,Terra Cotta - DK,#97382b
                                    169,Pewter Gray,#827d7d
                                    356, Terra Cotta - MED,#be5c4b
                                    208,Lavender - VY DK,#9442a7
                                    367, Pistachio Green - DK,#446b45
                                    209,Lavender - DK,#ba72c6
                                    368,Pistachio Green - LT,#7fc66d
                                    210,Lavender - MED,#d49fe1
                                    369,Pistachio Green - VY LT,#cdefa6
                                    211, Lavender - LT,#e5bded
                                    370,Mustard - MED,#917245
                                    221,Shell Pink - VY DK,#792631
                                    371, Mustard,#9f8352
                                    223, Shell Pink - LT,#bb6864
                                    372,Mustard - LT,#ad9564
                                    224,Shell Pink - VY LT,#e2a598
                                    400, Mahogany - DK,#813718
                                    225,Shell Pink - ULT VY LT,#f8d9cd
                                    402,Mahogany - VY LT,#ef9e74
                                    300, Mahogany - VY DK,#6c3116
                                    407, Desert Sand - DK,#b77159
                                    301,Mahogany - MED,#aa5237
                                    413,Pewter Gray - DK,#4a4749
                                    304,Red - MED,#a10c39
                                    414,Steel Gray - DK,#766e72
                                    307,Lemon,#fde949
                                    415,Pearl Gray,#b8b9bd
                                    309, Rose - DK,#ba2044
                                    420,Hazelnut Brown - DK,#855a30
                                    310,Black,#000000
                                    422,Hazelnut Brown - LT,#c99a67
                                    311,Blue - MED,#002a64
                                    433,Brown - MED,#73421e
                                    312,Baby Blue - VY DK,#1f3279
                                    434, Brown - LT,#8f5332
                                    435,Brown - VY LT,#a96538
                                    600, Cranberry - VY DK,#bf1c48
                                    436, Tan,#c78559
                                    601, Cranberry - DK,#c62a53
                                    437,Tan - LT,#daa26f
                                    602,Cranberry - MED,#d63f68
                                    444,Lemon - DK,#f5bc13
                                    603,Cranberry,#ed5d84
                                    445,Lemon - LT,#fcf999
                                    604,Cranberry - LT,#f793b2
                                    451,Shell Gray - DK,#887773
                                    605,Cranberry - VY LT,#fbacc4
                                    452, Shell Gray - MED,#ad9994
                                    606,Orange-red - BRIGHT,#f70f00
                                    453,Shell Gray - LT,#ccb8aa
                                    608,Orange - BRIGHT,#fd480c
                                    469,Avocado Green,#5b6533
                                    610, Drab Brown - DK,#6b5039
                                    470,Avocado Green - LT,#72813e
                                    611,Drab Brown,#7c5f46
                                    471, Avocado Green - VY LT,#9eb357
                                    612, Drab Brown - LT,#a6885e
                                    472,Avocado Green - ULT LT,#d1de75
                                    613, Drab Brown - VY LT,#b99f72
                                    498, Red - DK,#970b2c
                                    632,Desert Sand - ULT VY DK,#7f4232
                                    500,Blue Green - VY DK,#1d362a
                                    640, Beige Gray - VY DK,#817868
                                    501, Blue Green - DK,#2f5446
                                    642,Beige Gray - DK,#958d79
                                    502,Blue Green,#57826e
                                    644, Beige Gray - MED,#c4bea6
                                    503,Blue Green - MED,#89b89f
                                    645,Beaver Gray - VY DK,#5d5d54
                                    504, Blue Green - VY LT,#acdac1
                                    646, Beaver Gray - DK,#6b6860
                                    505,Grass Green - DK,#ceddc1
                                    647,Beaver Gray - MED,#908e85
                                    517,Wedgewood - DK,#216285
                                    648,Beaver Gray - LT,#a7a69f
                                    518,Wedgewood - LT,#50819c
                                    666,Red - BRIGHT,#ce1b33
                                    519,Sky Blue,#94b7cb
                                    676, Old Gold - LT,#ecbf7d
                                    520,Fern Green - DK,#384526
                                    677,Old Gold - VY LT,#f2dc9f
                                    522, Fern Green,#808b6e
                                    680,Old Gold - DK,#b07b46
                                    523,Fern Green - LT,#959f7a
                                    699,Green,#075b26
                                    524,Fern Green - VY LT,#aea78e
                                    700, Green - BRIGHT,#076c34
                                    535,Ash Gray - VY LT,#4b4b49
                                    701, Green - LT,#217c36
                                    543,Beige Brown - ULT VY LT,#ead0b5
                                    702,Kelly Green,#379130
                                    550, Violet - VY DK,#580e5c
                                    703, Chartreuse,#63b330
                                    552, Violet - MED,#902f99
                                    704,Chartreuse - BRIGHT,#88c53a
                                    553,Violet,#a449ac
                                    712,Cream,#f6efda
                                    554,Violet - LT,#dc9cde
                                    718,Plum,#cb2089
                                    561,Jade - VY DK,#285e48
                                    720, Orange Spice - DK,#c83a24
                                    562,Jade - MED,#3b8c5a
                                    721,Orange Spice - MED,#f46440
                                    563,Jade - LT,#6ed39a
                                    722,Orange Spice - LT,#f98756
                                    564,Jade - VY LT,#95e4af
                                    725, Topaz,#f9c15b
                                    580, Moss Green - DK,#355f0b
                                    726,Topaz - LT,#fddb63
                                    581,Moss Green,#838a29
                                    727, Topaz - VY LT,#fde98b
                                    597, Turquoise,#52adab
                                    728, Golden Yellow,#f2ae3f
                                    598,Turquoise - LT,#97d8d3
                                    729,Old Gold - MED,#ce9657
                                    730,Olive Green - VY DK,#63520b
                                    803, Blue - DEEP,#202754
                                    731,Olive Green - DK,#6b580b
                                    806,Peacock Blue - DK,#1d6c87
                                    732,Olive Green,#725c0c
                                    807, Peacock Blue,#558b9e
                                    733,Olive Green - MED,#a78a44
                                    809,Delft Blue,#919fd5
                                    734, Olive Green - LT,#bb9c54
                                    813,Blue - LT,#7fa0c6
                                    738,Tan - VY LT,#e2b783
                                    814, Garnet - DK,#711033
                                    739,Tan - ULT VY LT,#f2deb9
                                    815,Garnet - MED,#800b34
                                    740,Tangerine,#fd6f1a
                                    816,Garnet,#921238
                                    741,Tangerine - MED,#fc8b10
                                    817,Coral Red - VY DK,#bb1630
                                    742, Tangerine - LT,#fdae3c
                                    818,Baby Pink,#fededd
                                    743, Yellow - MED,#fdd769
                                    819,Baby Pink - LT,#fcebde
                                    744,Yellow - PALE,#fee88d
                                    820,Royal Blue - VY DK,#151264
                                    745, Yellow - LT PALE,#feeba5
                                    822, Beige Gray - LT,#e8dfc7
                                    746,Off White,#faf2d5
                                    823, Blue - DK,#000b44
                                    747,Sky Blue - VY LT,#cee9ea
                                    824, Blue - VY DK,#284779
                                    754, Peach - LT,#f7c9b0
                                    825,Blue - DK,#34588f
                                    758,Terra Cotta - VY LT,#e99f83
                                    826, Blue - MED,#5075a7
                                    760,Salmon,#ec8880
                                    827,Blue - VY LT,#a4c1de
                                    761, Salmon - LT,#f8b4ad
                                    828,Blue - ULT VY LT,#c3d7e6
                                    762,Pearl Gray - VY LT,#d1d0d2
                                    829, Golden Olive - VY DK,#64480c
                                    772, Yellow Green - VY LT,#d7efa7
                                    830, Golden Olive - DK,#6e501d
                                    775,Baby Blue - VY LT,#d4e3ef
                                    831, Golden Olive - MED,#7c5f20
                                    776,Pink - MED,#fca8ad
                                    832,Golden Olive,#9c7230
                                    777, Red - DEEP,#9b0042
                                    833,Golden Olive - LT,#b99956
                                    778,Antique Mauve - VY LT,#dca6a4
                                    834, Golden Olive - VY LT,#d2b468
                                    779, Brown,#53332d
                                    838, Beige Brown - VY DK,#4a3021
                                    780, Topaz - ULT VY DK,#945026
                                    839,Beige Brown - DK,#5a3c2d
                                    781,Topaz - VY DK,#a25f1f
                                    840, Beige Brown - MED,#7a5939
                                    782,Topaz - DK,#b26923
                                    841,Beige Brown - LT,#a37d64
                                    783,Topaz - MED,#d0883d
                                    842,Beige Brown - VY LT,#cbb094
                                    791, Cornflower Blue - VY DK,#2d2068
                                    844, Beaver Gray - ULT DK,#494842
                                    792, Cornflower Blue - DK,#454b8b
                                    868,Hazel Nut Brown,#995c30
                                    793,Cornflower Blue - MED,#7c82b5
                                    869,Hazelnut Brown - VY DK,#784c28
                                    794, Cornflower Blue - LT,#a0b2d7
                                    890,Pistachio Green - ULT DK,#324233
                                    796, Royal Blue - DK,#272276
                                    891,Carnation - DK,#ee3246
                                    797,Royal Blue,#2b3288
                                    892, Carnation - MED,#f44753
                                    798,Delft Blue - DK,#4e5ca7
                                    893,Carnation - LT,#f66879
                                    799,Delft Blue - MED,#6b7fc0
                                    894,Carnation - VY LT,#fd95a3
                                    800, Delft Blue - PALE,#b5c7e9
                                    895,Hunter Green - VY DK,#344b2e
                                    801, Coffee Brown - DK,#60391d
                                    898,Coffee Brown - VY DK,#532f1b
                                    899, Rose - MED,#ea6b78
                                    955,Nile Green - LT,#a8ebad
                                    900,Burnt Orange - DK,#c63117
                                    956,Geranium,#f7566d
                                    902,Garnet - VY DK,#651329
                                    957, Geranium - PALE,#fd99af
                                    904,Parrot Green - VY DK,#386324
                                    958, Seagreen - DK,#0db294
                                    905,Parrot Green - DK,#467924
                                    959,Seagreen - MED,#72d0b7
                                    906,Parrot Green - MED,#6c9e29
                                    961,Dusty Rose - DK,#de586c
                                    907,Parrot Green - LT,#9dc72d
                                    962,Dusty Rose - MED,#eb7183
                                    909,Emerald Green - VY DK,#106b43
                                    963, Dusty Rose - ULT VY LT,#fdccd1
                                    910,Emerald Green - DK,#10814e
                                    964,Seagreen - LT,#a5e4d4
                                    911,Emerald Green - MED,#109256
                                    966,Baby Green - MED,#94d28a
                                    912,Emerald Green - LT,#36b26b
                                    967,Peach - LT,#ffc2ac
                                    913,Nile Green - MED,#55ca7d
                                    970,Pumpkin - LT,#fb6721
                                    915,Plum - DK,#95085a
                                    971,Pumpkin,#fc670d
                                    917,Plum - MED,#ac1071
                                    972,Canary - DEEP,#fb9f11
                                    918,Red Copper - DK,#883630
                                    973,Canary - BRIGHT,#fccd2d
                                    919,Red Copper,#9b371b
                                    975, Golden Brown - DK,#813c11
                                    920,Copper - MED,#ab4836
                                    976,Golden Brown - MED,#cf7532
                                    921,Copper,#c0573d
                                    977,Golden Brown - LT,#ec8f43
                                    922,Copper - LT,#dd6e4c
                                    986,Forest Green - VY DK,#2e5230
                                    924, Gray Green - VY DK,#384a4a
                                    987, Forest Green - DK,#436838
                                    926,Gray Green - MED,#617674
                                    988,Forest Green - MED,#66924a
                                    927,Gray Green - LT,#9fa8a5
                                    989,Forest Green,#71a74e
                                    928, Gray Green - VY LT,#c0c6c0
                                    991, Aquamarine - DK,#135f55
                                    930,Antique Blue - DK,#495c6b
                                    992,Aquamarine - LT,#42b59e
                                    931,Antique Blue - MED,#667684
                                    993,Aquamarine - VY LT,#62d8b6
                                    932, Antique Blue - LT,#93a0af
                                    995,Electric Blue - DK,#0061b0
                                    934,Avocado Green - BLACK,#323324
                                    996,Electric Blue - MED,#49a8eb
                                    935,Avocado Green - DK,#383a2a
                                    3011,Khaki Green - DK,#655935
                                    936,Avocado Green - VY DK,#3f4227
                                    3012, Khaki Green - MED,#8b7b4e
                                    937,Avocado Green - MED,#434f2c
                                    3013,Khaki Green - LT,#afa97b
                                    938,Coffee Brown - ULT DK,#45271a
                                    3021, Brown Gray - VY DK,#50403b
                                    939, Blue - VY DK,#09092f
                                    3022, Brown Gray - MED,#848274
                                    943,Aquamarine - MED,#009a77
                                    3023,Brown Gray - LT,#a29b86
                                    945,Tawny,#f6c19a
                                    3024,Brown Gray - VY LT,#beb8ac
                                    946, Burnt Orange - MED,#ed4115
                                    3031,Mocha Brown - VY DK,#423014
                                    947, Burnt Orange,#fc4f16
                                    3032,Mocha Brown - MED,#9d8868
                                    948,Peach - VY LT,#fde6d3
                                    3033, Mocha Brown - VY LT,#dbc7ad
                                    950, Desert Sand - LT,#e5ac8d
                                    3041,Antique Violet - MED,#866a76
                                    951,Tawny - LT,#faddb6
                                    3042,Antique Violet - LT,#af98a0
                                    954,Nile Green,#6fda8a
                                    3045, Yellow Beige - DK,#af8152
                                    3046,Yellow Beige - MED,#ceb074
                                    3731,Dusty Rose - VY DK,#c34c5c
                                    3047, Yellow Beige - LT,#ead8ab
                                    3733,Dusty Rose,#ea7e86
                                    3051, Green Gray - DK,#4c4c1e
                                    3740,Antique Violet - DK,#71535d
                                    3052,Green Gray - MED,#787e5c
                                    3743,Antique Violet - VY LT,#cfc2c9
                                    3053, Green Gray,#999d75
                                    3746,Blue Violet - DK,#844ab5
                                    3064,Desert Sand,#ba7056
                                    3747, Blue Violet - VY LT,#d0c5ec
                                    3072, Beaver Gray - VY LT,#d2d2ca
                                    3750, Antique Blue - VY DK,#1d4552
                                    3078, Golden Yellow - VY LT,#fcf6b6
                                    3752, Antique Blue - VY LT,#bac9cc
                                    3325, Baby Blue - LT,#adcde7
                                    3753,Antique Blue - ULT VY LT,#d9e6ec
                                    3326,Rose - LT,#f9979c
                                    3755,Baby Blue(?),#81a5d8
                                    3328,Salmon - DK,#be444a
                                    3756,Baby Blue,#e9f4fa
                                    3340, Apricot - MED,#fd6b4f
                                    3760,Wedgewood - MED,#467293
                                    3341,Apricot,#fd8e78
                                    3761,Sky Blue - LT,#b1d0df
                                    3345,Hunter Green - DK,#40552e
                                    3765,Peacock Blue - VY DK,#175e78
                                    3346, Hunter Green,#56743b
                                    3766,Peacock Blue - LT,#4b8aa1
                                    3347,Yellow Green - MED,#6d9646
                                    3768,Gray Green - DK,#4c605f
                                    3348,Yellow Green - LT,#bedf74
                                    3770,Tawny - VY LT,#fef1d8
                                    3350, Dusty Rose - ULT DK,#aa3949
                                    3771, Peach - DK,#e8ac9b
                                    3354,Dusty Rose - LT,#efa5ac
                                    3772,Desert Sand - VY DK,#995744
                                    3362, Pine Green - DK,#49523c
                                    3773,Desert Sand - MED,#cf866d
                                    3363,Pine Green - MED,#617451
                                    3774,Desert Sand - VY LT,#f3cfb4
                                    3364, Pine Green,#8e9b6d
                                    3776,Mahogany - LT,#c96444
                                    3371,Black Brown,#36220e
                                    3777, Terra Cotta - VY DK,#922f25
                                    3607, Plum - LT,#d94c9d
                                    3778,Terra Cotta - LT,#d2705c
                                    3608,Plum - VY LT,#ec81be
                                    3779, Terra Cotta - ULT VY LT,#f2ab95
                                    3609,Plum - ULT LT,#f6b0df
                                    3781, Mocha Brown - DK,#593f2b
                                    3685,Mauve - VY DK,#79263b
                                    3782, Mocha Brown - LT,#b69d80
                                    3687,Mauve,#b5455d
                                    3787,Brown Gray - DK,#62524c
                                    3688,Mauve - MED,#dc7c86
                                    3790,Beige Gray - ULT DK,#6d5a4b
                                    3689, Mauve - LT,#f8bbc8
                                    3799,Pewter Gray - VY DK,#39393d
                                    3705, Melon - DK,#f2494f
                                    3801,Melon - VY DK,#e4353d
                                    3706, Melon - MED,#fd6e70
                                    3802,Antique Mauve - VY DK,#672a33
                                    3708, Melon - LT,#fda0ae
                                    3803,Mauve - DK,#872a43
                                    3712,Salmon - MED,#d95d5d
                                    3804,Cyclamen Pink - DK,#ce2b63
                                    3713,Salmon - VY LT,#fdd5d0
                                    3805, Cyclamen Pink,#df3c73
                                    3716,Dusty Rose - VY LT,#fcafb9
                                    3806, Cyclamen Pink - LT,#f15a91
                                    3721,Shell Pink - DK,#933b3d
                                    3807,Cornflower Blue,#4b599e
                                    3722, Shell Pink - MED,#a04b4c
                                    3808,Turquoise - ULT VY DK,#03535c
                                    3726,Antique Mauve - DK,#95565c
                                    3809,Turquoise - VY DK,#136a75
                                    3727, Antique Mauve - LT,#da9ea6
                                    3810,Turquoise - DK,#2d8d98
                                    3811,Turquoise - VY LT,#a8e2e5
                                    3839, Lavender Blue - MED,#7a7ec5
                                    3812,Seagreen - VY DK,#07a184
                                    3840, Lavender Blue - LT,#b2bdea
                                    3813,Blue Green - LT,#86c3ab
                                    3841,Baby Blue - PALE,#d9eaf2
                                    3814,Aquamarine,#0b8673
                                    3842,Wedgewood - DK,#06506a
                                    3815,Celadon Green - DK,#437259
                                    3843,Electric Blue,#28a3de
                                    3816, Celadon Green,#60937a
                                    3844,Bright Turquoise - DK,#1f7fa0
                                    3817,Celadon Green - LT,#81c6a4
                                    3845,Bright Turquoise - MED,#2badd1
                                    3818,Emerald Green - ULT VY DK,#005d2e
                                    3846,Bright Turquoise - LT,#5eccec
                                    3819,Moss Green - LT,#ccc959
                                    3847,Teal Green - DK,#186358
                                    3820,Straw - DK,#dba53e
                                    3848,Teal Green - MED,#207e72
                                    3821,Straw,#ebbb52
                                    3849,Teal Green - LT,#35b193
                                    3822,Straw - LT,#f7d169
                                    3850,Bright Green - DK,#208b46
                                    3823,Yellow - ULT PALE,#fef5cd
                                    3851, Bright Green - LT,#61bb84
                                    3824,Apricot - LT,#fcae99
                                    3852,Straw - VY DK,#e3a730
                                    3825, Pumpkin - PALE,#fea370
                                    3853,Autumn Gold - DK,#ef8125
                                    3826,Golden Brown,#b16633
                                    3854, Autumn Gold - MED,#fbac56
                                    3827,Golden Brown - PALE,#eaa664
                                    3855,Autumn Gold - LT,#fddfa0
                                    3828,Hazelnut Brown,#aa7c43
                                    3856, Mahogany - ULT VY LT,#fdbe8e
                                    3829,Old Gold - VY DK,#a7671d
                                    3857, Rosewood - DK,#6a2f26
                                    3830,Terra Cotta,#a94138
                                    3858, Rosewood - MED,#803a32
                                    3831,Raspberry - DK,#c12b52
                                    3859,Rosewood - LT,#ba7a6c
                                    3832,Raspberry - MED,#e36370
                                    3860,Cocoa,#896362
                                    3833,Raspberry - LT,#ea8b96
                                    3861,Cocoa - LT,#ac8583
                                    3834,Grape - DK,#6a2258
                                    3862,Mocha Beige - DK,#6e492a
                                    3835,Grape - MED,#924d78
                                    3863,Mocha Beige - MED,#94725d
                                    3836,Grape - LT,#c597b9
                                    3864,Mocha Beige - LT,#c9aa92
                                    3837,Lavender - ULT DK,#8a2a8f
                                    3865, Winter White,#fffdf9
                                    3838,Lavender Blue - DK,#606bad
                                    3866,Mocha Brown - ULT VY LT,#f0e6d7
                                    3880,Medium Very Dark Shell Pink,#793c37
                                    3881,Pale Avocado Green,#8fa463
                                    3882,Medium Light Cocoa,#674732
                                    3883,Medium Light Copper,#dd6f32
                                    3884,Medium Light Pewter,#676e6b
                                    3885,Medium Very Dark Blue,#054281
                                    3886, Very Dark Plum,#6c0d53
                                    3887, Ultra Very Dark Lavender,#633888
                                    3888,Medium Dark Antique Violet,#6b5b66
                                    3889, Medium Light Lemon,#f1dc4c
                                    3890, Very Light Bright Turquoise,#38cbee
                                    3891,Very Dark Bright Turquoise,#055f9d
                                    3892, Medium Light Orange Spice,#f66209
                                    3893,Very Light Mocha Beige,#cbad97
                                    3894, Very Light Parrot Green,#90ac09
                                    3895,Medium Dark Beaver Gray,#878471";
        private List<F> _fabrics = new List<F>
        {
                new F("Aida 16", 16, "Blockweave", 1, "100% cotton"),
                new F("Aida 14", 14, "Blockweave", 1, "100% cotton"),
                new F("Aida 18", 18, "Blockweave", 2, "100% cotton"),
                new F("Aida 11", 11, "Blockweave", 3, "100% cotton"),
            new F("Aida 10", 10, "Blockweave", 3, "100% cotton"),
                new F("Linda", 27, "Evenweave", 1, "100% cotton"),
                new F("Jubilee", 28, "Evenweave", 3, "100% cotton"),
                new F("Annabelle", 28, "Evenweave", 3, "100% cotton"),
                new F("Dublin", 20, "Evenweave", 3, "100% linen"),
                new F("Cashel", 28, "Evenweave", 2, "100% linen"),
                new F("Belfast", 32, "Evenweave", 2, "100% linen"),
                new F("Permin", 32, "Evenweave", 2, "100% linen"),
                new F("Edinburgh", 36, "Evenweave", 3, "100% linen"),
                new F("Newcastle", 40, "Evenweave", 3, "100% linen"),
                new F("Lugana", 25, "Evenweave", 1, "52% cotton & 48% rayon"),
                new F("Murano", 32, "Evenweave", 1, "52% cotton & 48% rayon"),
                new F("Bellana", 20, "Evenweave", 3, "52% cotton & 48% rayon"),
                new F("Perlleinen80", 20, "Evenweave", 3, "60% polyester & 40% linen"),
                new F("Perlleinen100", 25, "Evenweave", 3, "60% polyester & 40% linen"),
                new F("Perlleinen", 32, "Evenweave", 3, "52% cotton & 48% rayon"),
                new F("Brittney", 28, "Evenweave", 2, "52% cotton & 48% rayon"),
                new F("Lucan", 32, "Evenweave", 2, "48% cotton & 52% linen")
            };
        private List<SeedKit> _kits = new List<SeedKit>
        {
            new SeedKit{Title = "Русская усадьба. Чай под яблоней", Manufacturer = "Riolis", Item = "1140", Author = "Юлия Красавина", ColorsCount = 24, WidthSm = 30, HeightSm = 40,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 14", WidthStitches = 124, HeightStitches = 183, Image = "http://www.riolis.ru/zoom/photos/2177.jpg",
                Link = "http://www.riolis.ru/catalog/details_2177.html"
            },
            new SeedKit{Title = "Одуванчики", Manufacturer = "Riolis", Item = "807", Author = "Юлия Красавина", ColorsCount = 11, WidthSm = 30, HeightSm = 21,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 16", WidthStitches = 170, HeightStitches = 116, Image = "http://www.riolis.ru/zoom/photos/1038.jpg",
                Link = "http://www.riolis.ru/catalog/details_1038.html"
            },
            new SeedKit{Title = "Фуксия", Manufacturer = "Riolis", Item = "1398", Author = "Юлия Красавина", ColorsCount = 25, WidthSm = 40, HeightSm = 30,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 14", WidthStitches = 187, HeightStitches = 144, Image = "http://www.riolis.ru/zoom/photos/2878.jpg",
                Link = "http://www.riolis.ru/catalog/details_2878.html"
            },
            new SeedKit{Title = "Озеро в горах", Manufacturer = "Riolis", Item = "1235", Author = "Светлана Сидорова", ColorsCount = 29, WidthSm = 60, HeightSm = 40,
                ThreadManufacturer = "Riolis", FabricItem = "Aida 10", WidthStitches = 230, HeightStitches = 160, Image = "http://www.riolis.ru/zoom/photos/2450.jpg",
                Link = "http://www.riolis.ru/catalog/details_2450.html"
            },
            new SeedKit{Title = "Снегири", Manufacturer = "Золотое Руно", Item = "РС-018", Author = "Крумина Елена", ColorsCount = 33, WidthSm = 24.7m, HeightSm = 32.2m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 210, HeightStitches = 120, Image = "https://www.rukodelie.ru/upload/iblock/9c4/7a92849b_52b5_11e4_9997_bcaec538956e_2acde4e7_53a2_11e4_9997_bcaec538956e.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/rayskiy_sad/rs_018_snegiri"
            },
            new SeedKit{Title = "Волшебный лес", Manufacturer = "Золотое Руно", Item = "Ф-028", Author = "Крумина Елена", ColorsCount = 41, WidthSm = 36.9m, HeightSm = 46.7m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 300, HeightStitches = 239, Image = "https://www.rukodelie.ru/upload/iblock/bb3/5ad3767a_5757_11e3_a080_485b3970aae4_5ad3767b_5757_11e3_a080_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/fentezi/f_028_volshebnyy_les"
            },
            new SeedKit{Title = "Зимняя фантазия", Manufacturer = "Золотое Руно", Item = "ЧМ-032", Author = "Есенова Индира", ColorsCount = 30, WidthSm = 44.2m, HeightSm = 36.9m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 240, HeightStitches = 280, Image = "https://www.rukodelie.ru/upload/iblock/d67/7a9284d5_52b5_11e4_9997_bcaec538956e_a94f10c2_538b_11e4_9997_bcaec538956e.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/chudesnoe_mgnovenie/chm_032_zimnyaya_fantaziya"
            },
            new SeedKit{Title = "Уголок России", Manufacturer = "Золотое Руно", Item = "ВМ-021", Author = "Есенова Индира", ColorsCount = 42, WidthSm = 46.4m, HeightSm = 30.2m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 16", WidthStitches = 300, HeightStitches = 194, Image = "https://www.rukodelie.ru/upload/iblock/f3a/e3e48288_f8ef_11e2_af16_485b3970aae4_e3e48289_f8ef_11e2_af16_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/vremena_goda/vm_021_ugolok_rossii/"
            },
            new SeedKit{Title = "Мимоза", Manufacturer = "Золотое Руно", Item = "Т-004", Author = "Сафонова Надежда", ColorsCount = 26, WidthSm = 62m, HeightSm = 34.7m,
                ThreadManufacturer = "Madeira", FabricItem = "Aida 14", WidthStitches = 300, HeightStitches = 200, Image = "https://www.rukodelie.ru/upload/iblock/52d/e3e4828a_f8ef_11e2_af16_485b3970aae4_e3e4828b_f8ef_11e2_af16_485b3970aae4.resize1.jpg",
                Link = "https://www.rukodelie.ru/catalog/nabory_dlya_vyshivaniya/nabory_dlya_vyshivaniya_krestom/triptikh/t_004_mimoza/"
            },
            new SeedKit{Title = "Тюльпаны", Manufacturer = "Алиса", Item = "2-29", Author = "Левашов Игорь", ColorsCount = 35, WidthSm = 40, HeightSm = 30,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 221, HeightStitches = 165, Image = "https://www.alisa-collection.ru/_data/objects/0001/3186/icon.jpg",
                Link = "https://www.alisa-collection.ru/?id=13186"
            },
            new SeedKit{Title = "Февральский домик", Manufacturer = "Алиса", Item = "3-22", Author = null, ColorsCount = 24, WidthSm = 18, HeightSm = 14,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 16", WidthStitches = 112, HeightStitches = 90, Image = "https://www.alisa-collection.ru/_data/objects/0001/9206/icon.jpg",
                Link = "https://www.alisa-collection.ru/?id=19206"
            },
            new SeedKit{Title = "Венок сновидений", Manufacturer = "Овен", Item = "919", Author = "Левашов Игорь", ColorsCount = 26, WidthSm = 36, HeightSm = 36,
                ThreadManufacturer = "ПНК им. Кирова", FabricItem = "Aida 16", WidthStitches = 230, HeightStitches = 230, Image = "http://ooo-oven.ru/upload/iblock/bee/bee22d3c003d59f279ba209a8b9fec18.jpg",
                Link = "https://www.stitch.su/oven/919"
            },
            new SeedKit{Title = "Tiger chilling out", Manufacturer = "Dimensions", Item = "35222", Author = "Matthew Hillier", ColorsCount = 20, WidthSm = 23, HeightSm = 36,
                ThreadManufacturer = "Dimensions", FabricItem = "Aida 18", WidthStitches = 160, HeightStitches = 250, Image = "https://www.dimshop.ru/images/big/35222.jpg",
                Link = "https://www.dimshop.ru/dimensions/35222/"
            },
            new SeedKit{Title = "Аромат сирени", Manufacturer = "Чудесная игла", Item = "40-64", Author = null, ColorsCount = 41, WidthSm = 40, HeightSm = 37,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 220, HeightStitches = 210, Image = "http://chudo-igla.ru/thumb/2/_6AtVZioQC4eCqJE3kVblQ/c/d/6aromat-sireni.jpg",
                Link = "https://www.stitch.su/chudesnaya_igla/40-64"
            },
            new SeedKit{Title = "Моя отрада!", Manufacturer = "Чудесная игла", Item = "90-01", Author = null, ColorsCount = 45, WidthSm = 32, HeightSm = 41,
                ThreadManufacturer = "Gamma", FabricItem = "Aida 14", WidthStitches = 164, HeightStitches = 225, Image = "http://chudo-igla.ru/thumb/2/bIled3wWBnJ_74DyzsJVnw/c/d/90-01_baget.jpg",
                Link = "https://www.stitch.su/chudesnaya_igla/90-01"
            },
            new SeedKit{Title = "Узоры зимы", Manufacturer = "М.П. Студия", Item = "Р-162", Author = "Слесарева Дарья", ColorsCount = 9, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/437/4379779c252b82a61e37ccdc0683f48d.jpg",
                Link = "https://mpstudia.ru/shop/57/1732/"
            },
            new SeedKit{Title = "Новогодний изумруд", Manufacturer = "М.П. Студия", Item = "Р-165", Author = "Слесарева Дарья", ColorsCount = 10, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/7b3/7b385b5832d3d10e9d6d4891584aff89.jpg",
                Link = "https://mpstudia.ru/shop/57/1722/"
            },
            new SeedKit{Title = "Янтарное великолепие", Manufacturer = "М.П. Студия", Item = "Р-168", Author = "Слесарева Дарья", ColorsCount = 8, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/688/6884d9f609d1bd7d07a78afd9ed280b9.jpg",
                Link = "https://mpstudia.ru/shop/57/1750/"
            },
            new SeedKit{Title = "Морозный рубин", Manufacturer = "М.П. Студия", Item = "Р-167", Author = "Слесарева Дарья", ColorsCount = 11, WidthSm = 9, HeightSm = 9,
                ThreadManufacturer = "Bestex", FabricItem = "Aida 16", WidthStitches = 55, HeightStitches = 55, Image = "https://mpstudia.ru/upload/iblock/2a2/2a292cf6efd1660a3c1bc75a45d225f8.jpg",
                Link = "https://mpstudia.ru/shop/57/1731/"
            },
            new SeedKit{Title = "Морозный рубин", Manufacturer = "RTO", Item = "М553", Author = "Сотникова Ольга", ColorsCount = 25, WidthSm = 25.5m, HeightSm = 21.5m,
                ThreadManufacturer = "DMC", FabricItem = "Aida 14", WidthStitches = 140, HeightStitches = 120, Image = "https://rto21.by/upload/iblock/9e3/9e350a6127e755752aec5c6482fe0ed0.jpg",
                Link = "https://rto21.by/catalog/rto/m553_nabor_dlya_vyshivaniya_koshachya_gratsiya/"
            },
        };

        private string _threadsUniverse = @"301 9210 0,9
775 2602 0,3
350 0810 0,1
828 3302 0,7
351 9222 0,2 
839 6512 6,2
352 9220 0,7
840 6006 2,1
353 7501 4,1
841 6203 10,3
400 9212 0,6
842 6501 5,6
402 9208 6,0
921 5408 0,2
420 9104 0,2
938 5711 1,3
437 5903 2,7
945 6300 14,7
518 3304 0,4
948 9000 19,6
519 2902 0,8
3078 0300 6,4
712 0103  14,3
3371 6516 1,0
738 5902  26,7
3770 9207 17,5
739 5901  28,6
3776 9210 2,1
743 0508  6,1
3821 7306   3,2
744 0504  12,3
3823 0501 33,0
745 0502  4,8
3865 0104 30,6
754 1602  1,6
B5200 0101 1,1";
        #endregion
        public DataSeed(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
            _lazyLoader = null;
        }

        public void Execute()
        {
            //if (File.Exists(FileName))
            //{
            //    var result = File.ReadAllText(FileName);
            //    var kits = JsonConvert.DeserializeObject<IEnumerable<KitModel>>(result).ToArray();

            //    AddKitManufacturers(kits);
            //    AddPatternAuthors(kits);
            //    AddPatterns(kits);
            //}

            //AddFabricTypes();
            //AddFabricContentTypes();
            //AddFabrics();
            //AddKits();
            //AddDmcPalette();
            //AddPnkPalette();
            //AddPatternThreads();
            //_dbContext.SaveChanges();

            //AddPatternColors();
           // _dbContext.SaveChanges();
            new PatternSeed().Execute(_dbContext);
        }

        private void AddPatternColors()
        {
            string s = @"154 7,2
211 1,8
316 1,3
369 3,5
413 2,2 
554 1,8
562 1,8
563 2,2
564 0,7
597 1,1 
598 0,9
648 1,7
677 0,3
712 0,9
727 0,4 
746 0,3
760 0,2
761 1
775 2,3
778 4,6 
796 0,1
799 0,2
809 0,5
819 8,2
832 0,3 
833 0,4
834 0,5
905 0,8
906 1,4
907 1,9 
963 1,6
964 0,9
966 0,5
987 0,5
988 0,6 
3072 1,1
3078 0,5
3348 0,4
3687 1,8
3688 2,2 
3689 1,5
3713 6,2
3726 2,8
3727 1,1
3756 2,7 
3770 1,1
3810 1,0
3834 9
3835 3,5
3836 3,6 
3838 0,9
3840 0,8
3841 1,7
B5200 0,6
Blanc 5,3  ";
            var datalines = s.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dmcArray = new List<ThreadColorOption>(datalines.Length);
            var pattern = _dbContext.Patterns.First(p => p.Item == "21749");
            foreach (var line in datalines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToArray();
                if (parts.Length != 2)
                {
                    throw new Exception();
                }

                var dmc = _dbContext.ThreadColors.First(c => c.ColorId == parts[0]);
                if (dmc == null)
                {
                    throw new Exception();
                }

                var length = decimal.Parse(parts[1]);
                dmcArray.Add(new ThreadColorOption(_lazyLoader)
                    {Pattern = pattern, ThreadColor = dmc, RequiredLength = length});
            }

            _dbContext.ThreadColorOptions.AddRange(dmcArray);
        }

        private void AddPatternThreads()
        {
            _dbContext.ThreadColors.Load();
               var datalines = _threadsUniverse.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            var dmcArray = new List<ThreadColorOption>(datalines.Length);
            var pnkArray = new List<ThreadColorOption>(datalines.Length);
            var pattern = _dbContext.Patterns.First(p => p.Title == "Вселенная");
            foreach (var line in datalines)
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
                var dmc = _dbContext.ThreadColors.Local.First(c => c.ColorId == parts[0]);
                var pnk = _dbContext.ThreadColors.Local.First(c => c.ColorId == parts[1]);
                var length = decimal.Parse(parts[2]);
                dmcArray.Add(new ThreadColorOption(_lazyLoader){Pattern = pattern, ThreadColor = dmc, RequiredLength = length });
                var existingPnk = pnkArray.FirstOrDefault(p => p.ThreadColor == pnk);
                if (existingPnk == null)
                {
                    pnkArray.Add(new ThreadColorOption(_lazyLoader) { Pattern = pattern, ThreadColor = pnk, RequiredLength = length });
                }
                else
                {
                    existingPnk.RequiredLength += length;
                }
            }
            _dbContext.ThreadColorOptions.AddRange(dmcArray);
            foreach (var option in pnkArray)
            {
                _dbContext.ThreadColorOptions.Add(option);
            }
        }

        private void AddPnkPalette()
        {

            var dtoThreads = new List<ThreadColor>();
            var content = File.ReadAllText("pnk.json");
            var jsonArray = JsonConvert.DeserializeObject(content) as JArray;
            var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name.StartsWith("ПНК"));
            foreach (var token in jsonArray)
            {
                var dto = new ThreadColor(_lazyLoader);
                dto.Manufacturer = manufacturer;
                var name = token["Name"].Value<string>();
                dto.ColorId = name;
                dto.ColorName = name;
                var rgb = token["Rgb"].Value<string>();
                dto.RgbColor = rgb;
                dto.Length = 10;
                dto.Sku = "";
                dtoThreads.Add(dto);
            }
            _dbContext.ThreadColors.AddRange(dtoThreads);
        }

        private void AddDmcPalette()
        {
            var dtoThreads = new List<ThreadColor>();
            var manufacturer = _dbContext.ThreadManufacturers.First(m => m.Name == "DMC");
            foreach (var thread in _dmcColors.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                var parts = thread.Split(',').Select(t => t.Trim()).ToArray();
                var dto = new ThreadColor(_lazyLoader);
                dto.Manufacturer = manufacturer;
                dto.ColorId = parts[0];
                dto.ColorName = parts[1];
                dto.RgbColor = parts[2];
                dto.Length = 8;
                dto.Sku = "117S";
                dtoThreads.Add(dto);
            }

            _dbContext.ThreadColors.AddRange(dtoThreads);
        }

        private void AddKits()
        {
            _dbContext.PatternAuthors.Load();
            _dbContext.KitManufacturers.Load();
            _dbContext.ThreadManufacturers.Load();
            _dbContext.Fabrics.Load();
            _dbContext.FabricItems.Load();
            var dtoKits = new List<Kit>();
            foreach (var seedKit in _kits.Skip(1))
            {
                var dto = new Kit((_lazyLoader));
                dto.Title = seedKit.Title;
                var author = _dbContext.PatternAuthors.Local.FirstOrDefault(a => a.Name == seedKit.Author);
                if (author == null && !string.IsNullOrEmpty(seedKit.Author))
                {
                    author = new PatternAuthor((_lazyLoader)) { Name = seedKit.Author };
                    _dbContext.PatternAuthors.Add(author);
                }
                dto.Author = author;
                dto.ColorsCount = seedKit.ColorsCount;
                dto.Item = seedKit.Item;
                var manufacturer = _dbContext.KitManufacturers.Local.FirstOrDefault(a => a.Name.ToLowerInvariant() == seedKit.Manufacturer.ToLowerInvariant());
                if (manufacturer == null)
                {
                    manufacturer = new KitManufacturer((_lazyLoader)) { Name = seedKit.Manufacturer };
                    _dbContext.KitManufacturers.Add(manufacturer);
                }
                dto.Manufacturer = manufacturer;
                dto.Image = seedKit.Image;
                dto.Link = seedKit.Link;
                dto.WidthSm = seedKit.WidthSm;
                dto.HeightSm = seedKit.HeightSm;
                dto.WidthStitches = seedKit.WidthStitches;
                dto.HeightStitches = seedKit.HeightStitches;
                var threadManufacturer = _dbContext.ThreadManufacturers.Local.FirstOrDefault(m => m.Name.ToLowerInvariant() == seedKit.ThreadManufacturer.ToLowerInvariant());
                if (threadManufacturer == null)
                {
                    threadManufacturer = new ThreadManufacturer(_lazyLoader) { Name = seedKit.ThreadManufacturer };
                    _dbContext.ThreadManufacturers.Add(threadManufacturer);
                }
                dto.ThreadManufacturer = threadManufacturer;
                var fabricItem = _dbContext.FabricItems.Local.FirstOrDefault(f => f.Fabric != null && f.Fabric.Name == seedKit.FabricItem);
                if (fabricItem == null)
                {
                    fabricItem = new FabricItem((_lazyLoader)) { Fabric = _dbContext.Fabrics.First(f => f.Name == seedKit.FabricItem), Sku = "KitModel", ColorId = "-", ColorName = "-" };
                    _dbContext.FabricItems.Add(fabricItem);
                }
                dto.FabricItem = fabricItem;
                dtoKits.Add(dto);
            }

            _dbContext.Kits.AddRange(dtoKits);
        }

        private void AddFabrics()
        {
            var fabricsDto = new List<Fabric>(_fabrics.Count);
            foreach (var fabric in _fabrics)
            {
                var dto = new Fabric(_lazyLoader);
                dto.Name = fabric.Name;
                dto.Count = fabric.Count;
                dto.Priority = fabric.Priority;
                dto.FabricTypeId = _dbContext.FabricTypes.First(f => f.Name == fabric.Type).Id;
                dto.ContentTypeId = _dbContext.ContentTypes.First(f => f.Name == fabric.Content).Id;
                fabricsDto.Add(dto);
            }

            _dbContext.Fabrics.AddRange(fabricsDto);
        }

        private void AddFabricContentTypes()
        {
            var contentTypes = _fabrics
                .Select(k => k.Content)
                .Distinct()
                .Select(a => new ContentType(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.ContentTypes.AddRange(contentTypes);
        }

        private void AddFabricTypes()
        {
            var fabricTypes = _fabrics
                .Select(k => k.Type)
                .Distinct()
                .Select(a => new FabricType(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.FabricTypes.AddRange(fabricTypes);
        }

        private void AddPatterns(IEnumerable<KitModel> kits)
        {
            var dtoPatterns = new List<Pattern>();
            foreach (var kit in kits.Where(k => k.KitType == KitType.DesignerPattern))
            {
                var dto = new Pattern((_lazyLoader));
                dto.Title = kit.Title;
                dto.AuthorId = _dbContext.PatternAuthors.First(f => f.Name == kit.Manufacturer).Id;
                dto.Item = kit.Item;
                dto.ColorsCount = 0;
                dto.Width = (short)kit.Size.Width;
                dto.Height = (short)kit.Size.Height;
                dto.Image = kit.ImageUrl;
                dto.Link = String.Empty;
                dtoPatterns.Add(dto);
            }

            _dbContext.Patterns.AddRange(dtoPatterns);
        }

        private void AddPatternAuthors(IEnumerable<KitModel> kits)
        {
            var authors = kits
                .Where(k => k.KitType == KitType.DesignerPattern)
                .Select(k => k.Manufacturer)
                .Distinct()
                .Select(a => new PatternAuthor(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.PatternAuthors.AddRange(authors);
        }

        private void AddKitManufacturers(IEnumerable<KitModel> kits)
        {
            var manufacturers = kits
                .Where(k => k.KitType == KitType.ManufacturerKit)
                .Select(k => k.Manufacturer)
                .Distinct()
                .Select(a => new KitManufacturer(_lazyLoader) { Name = a })
                .ToArray();
            _dbContext.KitManufacturers.AddRange(manufacturers);
        }
    }
}
