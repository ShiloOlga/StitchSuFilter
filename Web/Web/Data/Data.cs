using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Data
{
    public static class Data
    {
        public const string DmcColors = @"Ecru,Ecru/off-white,#fff7e7
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

        public static string PnkColors = @"0101,0101,#ffffff
                                    0102,0102,#dcd9cb
                                    0103,0103,#d8d1be
                                    0104,0104,#dcd6c6
                                    0200,0200,#e4e1c2
                                    0202,0202,#dadc99
                                    0206,0206,#dfdc70
                                    0207,0207,#e2d933
                                    0208,0208,#dfc600
                                    0209,0209,#e5cf00
                                    0300,0300,#ddd59f
                                    0301,0301,#dfd381
                                    0303,0303,#e4cd56
                                    0304,0304,#d9bb5f
                                    0305,0305,#e5be20
                                    0306,0306,#d0a536
                                    0402,0402,#dabd74
                                    0404,0404,#d2aa51
                                    0501,0501,#e2dbb3
                                    0502,0502,#e6dba4
                                    0504,0504,#e2cc7e
                                    0506,0506,#e3c668
                                    0508,0508,#deb74d
                                    0510,0510,#dfaa2e
                                    0512,0512,#cf9340
                                    0600,0600,#e2cfb4
                                    0602,0602,#dfc3a5
                                    0604,0604,#e3b07c
                                    0606,0606,#e2914e
                                    0607,0607,#e28f30
                                    0608,0608,#e28533
                                    0701,0701,#e6bdaa
                                    0702,0702,#e8b190
                                    0704,0704,#e4966d
                                    0706,0706,#e2894f
                                    0709,0709,#d16c3f
                                    0710,0710,#d97733
                                    0711,0711,#d06a21
                                    0712,0712,#c95c36
                                    0713,0713,#bd5e14
                                    0716,0716,#b43b33
                                    0800,0800,#e5b0a1
                                    0801,0801,#e5b0a3
                                    0802,0802,#dd9783
                                    0804,0804,#d5766b
                                    0806,0806,#c86358
                                    0808,0808,#cb5b52
                                    0810,0810,#ba413c
                                    0811,0811,#ac3536
                                    0902,0902,#ab3a4b
                                    0904,0904,#a22e39
                                    0905,0905,#9f263b
                                    0906,0906,#a12433
                                    0907,0907,#8f3b42
                                    0908,0908,#8a2e3d
                                    0909,0909,#7f2739
                                    1001,1001,#eae1e3
                                    1002,1002,#dfcbcf
                                    1004,1004,#e0bcc1
                                    1005,1005,#d3b0be
                                    1006,1006,#dea7b4
                                    1008,1008,#cc8f9e
                                    1010,1010,#d68298
                                    1011,1011,#d47c7b
                                    1012,1012,#cf7983
                                    1013,1013,#cc5e64
                                    1014,1014,#c64c57
                                    1102,1102,#dccccf
                                    1104,1104,#d49eb8
                                    1106,1106,#c9769d
                                    1108,1108,#c8608e
                                    1110,1110,#b84073
                                    1112,1112,#9b2267
                                    1200,1200,#872741
                                    1201,1201,#7a2538
                                    1204,1204,#662530
                                    1206,1206,#6c2248
                                    1208,1208,#641d38
                                    1302,1302,#673e4d
                                    1303,1303,#562f3b
                                    1304,1304,#4e303b
                                    1402,1402,#dacace
                                    1404,1404,#c687b0
                                    1406,1406,#bf79a6
                                    1408,1408,#b86098
                                    1410,1410,#a6508c
                                    1411,1411,#8b2f69
                                    1412,1412,#733157
                                    1502,1502,#c37f9d
                                    1504,1504,#ba7290
                                    1506,1506,#a35375
                                    1507,1507,#9b4864
                                    1508,1508,#98546d
                                    1510,1510,#803c58
                                    1511,1511,#762a3d
                                    1602,1602,#dbb39e
                                    1604,1604,#c28060
                                    1606,1606,#ba6a4b
                                    1608,1608,#af5e3d
                                    1610,1610,#b96b50
                                    1612,1612,#a0543d
                                    1614,1614,#884432
                                    1616,1616,#72312a
                                    1702,1702,#cdb0c8
                                    1704,1704,#bf95bb
                                    1706,1706,#b27fac
                                    1708,1708,#9f6098
                                    1710,1710,#894985
                                    1712,1712,#783970
                                    1714,1714,#4e2d41
                                    1802,1802,#ad8fa1
                                    1804,1804,#a47d91
                                    1806,1806,#86566e
                                    1808,1808,#753d54
                                    1902,1902,#c1adb4
                                    1904,1904,#af939d
                                    1906,1906,#9c7e8b
                                    1908,1908,#8e6b78
                                    1909,1909,#694146
                                    1910,1910,#694756
                                    2001,2001,#a2879e
                                    2002,2002,#9c7ea0
                                    2003,2003,#7e5776
                                    2004,2004,#684f72
                                    2005,2005,#5b3551
                                    2006,2006,#3f2639
                                    2102,2102,#cfc0cc
                                    2104,2104,#c7b1ca
                                    2106,2106,#a17faf
                                    2108,2108,#8c6093
                                    2110,2110,#784680
                                    2112,2112,#67376f
                                    2202,2202,#b7a7c6
                                    2203,2203,#ab94ba
                                    2204,2204,#9a80af
                                    2206,2206,#85679b
                                    2208,2208,#72568d
                                    2210,2210,#5d407d
                                    2212,2212,#4f366b
                                    2214,2214,#4e3165
                                    2302,2302,#b8b1c9
                                    2304,2304,#9d92bc
                                    2306,2306,#877cae
                                    2308,2308,#69578f
                                    2310,2310,#514176
                                    2312,2312,#413368
                                    2402,2402,#bfbfcd
                                    2403,2403,#abb2c9
                                    2404,2404,#888db4
                                    2405,2405,#838cb1
                                    2406,2406,#7175a8
                                    2407,2407,#575e93
                                    2408,2408,#545990
                                    2410,2410,#464882
                                    2411,2411,#1c2f68
                                    2412,2412,#343b69
                                    2413,2413,#2d3567
                                    2414,2414,#312f4d
                                    2415,2415,#343247
                                    2501,2501,#a2b9ce
                                    2502,2502,#97b2cc
                                    2504,2504,#7594ba
                                    2506,2506,#6384b1
                                    2507,2507,#5470a0
                                    2508,2508,#4d72a4
                                    2602,2602,#c3ced8
                                    2604,2604,#a5b5c4
                                    2606,2606,#8399b9
                                    2608,2608,#647aa6
                                    2610,2610,#445b91
                                    2612,2612,#394673
                                    2614,2614,#2a3a61
                                    2701,2701,#b0c0c8
                                    2702,2702,#b2c3d2
                                    2704,2704,#a5c1d7
                                    2706,2706,#7ea7d0
                                    2708,2708,#5383bc
                                    2710,2710,#4073b3
                                    2712,2712,#2a68a2
                                    2714,2714,#235398
                                    2802,2802,#b8c2ca
                                    2804,2804,#9aa9bb
                                    2806,2806,#7b8da4
                                    2808,2808,#5e7189
                                    2810,2810,#4b5e77
                                    2812,2812,#32455f
                                    2814,2814,#29384f
                                    2902,2902,#8ba1b2
                                    2904,2904,#46647f
                                    2906,2906,#385470
                                    2908,2908,#223e56
                                    3002,3002,#bdd6d4
                                    3004,3004,#93c9d1
                                    3008,3008,#5fadc3
                                    3010,3010,#2988a3
                                    3011,3011,#1877a4
                                    3100,3100,#a7c0cc
                                    3102,3102,#94bacd
                                    3104,3104,#6ca0c1
                                    3106,3106,#4e90b4
                                    3202,3202,#91b6cd
                                    3204,3204,#6294bb
                                    3206,3206,#4d7ea9
                                    3300,3300,#bfd3d7
                                    3302,3302,#abc7cb
                                    3304,3304,#4e7c9d
                                    3306,3306,#3d6f8a
                                    3401,3401,#9fb8bb
                                    3402,3402,#7a9a9f
                                    3403,3403,#7498a6
                                    3404,3404,#5e7f88
                                    3406,3406,#3e6a7c
                                    3408,3408,#416268
                                    3410,3410,#3a6274
                                    3502,3502,#bbd3ce
                                    3504,3504,#98bdb8
                                    3506,3506,#79adaa
                                    3508,3508,#62a2a1
                                    3510,3510,#448d8d
                                    3514,3514,#2f7a7b
                                    3600,3600,#a5bcbf
                                    3602,3602,#688f8e
                                    3606,3606,#4e7474
                                    3607,3607,#345e5f
                                    3608,3608,#3d6760
                                    3610,3610,#316c5f
                                    3702,3702,#566452
                                    3704,3704,#4c5e4d
                                    3800,3800,#6d806e
                                    3802,3802,#5d6e5d
                                    3804,3804,#485545
                                    3805,3805,#3f5038
                                    3806,3806,#37483e
                                    3807,3807,#2f3f2f
                                    3808,3808,#303e3c
                                    3901,3901,#bfd3b4
                                    3902,3902,#a2c4a4
                                    3904,3904,#94b788
                                    3906,3906,#6ea070
                                    3907,3907,#6a8d6e
                                    3908,3908,#638c4f
                                    3909,3909,#7f9944
                                    3910,3910,#527a43
                                    3912,3912,#4a6645
                                    3913,3913,#506f54
                                    3914,3914,#3d5838
                                    4002,4002,#cad2a6
                                    4004,4004,#b5c494
                                    4005,4005,#9cb081
                                    4006,4006,#879d64
                                    4008,4008,#76853c
                                    4010,4010,#71864c
                                    4102,4102,#aecec0
                                    4103,4103,#619e83
                                    4104,4104,#77ad95
                                    4106,4106,#81b28e
                                    4107,4107,#487f53
                                    4108,4108,#487a5a
                                    4109,4109,#467342
                                    4110,4110,#31694b
                                    4111,4111,#2e5e3b
                                    4112,4112,#2f5541
                                    4202,4202,#314136
                                    4204,4204,#2e403e
                                    4304,4304,#8f8f73
                                    4306,4306,#49553c
                                    4308,4308,#3a4330
                                    4402,4402,#99a280
                                    4404,4404,#7e8a61
                                    4406,4406,#797d54
                                    4408,4408,#575637
                                    4500,4500,#b9b567
                                    4501,4501,#abae75
                                    4502,4502,#9c9e54
                                    4503,4503,#808e4d
                                    4504,4504,#80824c
                                    4505,4505,#7a7333
                                    4506,4506,#736e3e
                                    4508,4508,#515133
                                    4510,4510,#42422a
                                    4602,4602,#afb18e
                                    4604,4604,#878d62
                                    4608,4608,#6e784d
                                    4609,4609,#666b3a
                                    4702,4702,#cfd66f
                                    4704,4704,#c1cf5f
                                    4706,4706,#acc347
                                    4707,4707,#9db335
                                    4708,4708,#7f9329
                                    4802,4802,#d0d299
                                    4803,4803,#c2b76a
                                    4804,4804,#c0bd64
                                    4805,4805,#918a35
                                    4806,4806,#8a9638
                                    4902,4902,#cada8a
                                    4904,4904,#bad15a
                                    5000,5000,#d8cd9c
                                    5001,5001,#bcab60
                                    5002,5002,#b19954
                                    5004,5004,#a48133
                                    5006,5006,#b2821c
                                    5102,5102,#8c886b
                                    5104,5104,#6f6b4f
                                    5200,5200,#a3966c
                                    5201,5201,#908359
                                    5202,5202,#847448
                                    5203,5203,#756336
                                    5204,5204,#645938
                                    5301,5301,#b3a154
                                    5302,5302,#826b2b
                                    5303,5303,#8e793e
                                    5304,5304,#786e38
                                    5306,5306,#6e5934
                                    5401,5401,#dbb774
                                    5402,5402,#d3a663
                                    5404,5404,#c7914f
                                    5405,5405,#cc8f52
                                    5406,5406,#c4814c
                                    5408,5408,#b86d3d
                                    5502,5502,#d1b375
                                    5504,5504,#bb9556
                                    5506,5506,#9d703b
                                    5601,5601,#c5a69a
                                    5602,5602,#c1908b
                                    5603,5603,#b18c86
                                    5604,5604,#b78279
                                    5605,5605,#9b5f60
                                    5606,5606,#8e4f4e
                                    5610,5610,#7b3f3b
                                    5702,5702,#b49491
                                    5704,5704,#9c7872
                                    5708,5708,#745155
                                    5709,5709,#623936
                                    5710,5710,#463535
                                    5711,5711,#45322a
                                    5802,5802,#c6a985
                                    5803,5803,#d3b085
                                    5804,5804,#c2a07e
                                    5806,5806,#ab7d56
                                    5808,5808,#9e6e4b
                                    5810,5810,#794c36
                                    5812,5812,#4e241a
                                    5901,5901,#d6c59e
                                    5902,5902,#d3be9c
                                    5903,5903,#be9a66
                                    5904,5904,#bfa181
                                    5905,5905,#ac8449
                                    5906,5906,#9f795b
                                    5907,5907,#8d6430
                                    5910,5910,#815940
                                    5911,5911,#6c492f
                                    5912,5912,#5b3d31
                                    6001,6001,#c6bca4
                                    6002,6002,#ac9f8b
                                    6004,6004,#988871
                                    6006,6006,#8c795f
                                    6007,6007,#756551
                                    6008,6008,#8b7359
                                    6012,6012,#84674d
                                    6100,6100,#c5bfab
                                    6102,6102,#b6a68b
                                    6104,6104,#a99879
                                    6106,6106,#9c8466
                                    6200,6200,#cfc0b0
                                    6202,6202,#bfa487
                                    6203,6203,#a9967f
                                    6204,6204,#9b785f
                                    6206,6206,#937059
                                    6207,6207,#887355
                                    6300,6300,#d5bfa9
                                    6301,6301,#caaf9a
                                    6302,6302,#af8878
                                    6304,6304,#9e7360
                                    6306,6306,#a37057
                                    6400,6400,#dcd7ca
                                    6401,6401,#b6aba2
                                    6402,6402,#a89891
                                    6404,6404,#99877b
                                    6406,6406,#7b6b5f
                                    6501,6501,#c0b29c
                                    6502,6502,#b7a895
                                    6504,6504,#988273
                                    6506,6506,#846e63
                                    6508,6508,#6b533b
                                    6509,6509,#6c563c
                                    6510,6510,#69533b
                                    6511,6511,#574b39
                                    6512,6512,#5b4841
                                    6513,6513,#4e3b2f
                                    6514,6514,#4c352e
                                    6515,6515,#564638
                                    6516,6516,#382d2c
                                    6600,6600,#b4b2a9
                                    6601,6601,#a4a298
                                    6602,6602,#b0a791
                                    6604,6604,#968c74
                                    6606,6606,#8c836b
                                    6608,6608,#837a63
                                    6700,6700,#c1c1b6
                                    6702,6702,#a5a596
                                    6704,6704,#919587
                                    6706,6706,#878a7a
                                    6708,6708,#6f6a5e
                                    6712,6712,#50463b
                                    6801,6801,#b6b6ab
                                    6802,6802,#a3a6a2
                                    6804,6804,#838782
                                    6806,6806,#62625f
                                    6808,6808,#5a5b55
                                    6902,6902,#b3bcc1
                                    6904,6904,#96a2b0
                                    6906,6906,#7b8a9c
                                    6908,6908,#526073
                                    6910,6910,#414a56
                                    7000,7000,#e8ebe8
                                    7001,7001,#a5a2a5
                                    7002,7002,#adb2b3
                                    7003,7003,#888b98
                                    7004,7004,#6a6b75
                                    7006,7006,#919ba3
                                    7008,7008,#525261
                                    7102,7102,#bcc0c0
                                    7104,7104,#a2abb4
                                    7106,7106,#8e98a1
                                    7108,7108,#828d99
                                    7110,7110,#5b6064
                                    7112,7112,#494f54
                                    7114,7114,#3d444f
                                    7201,7201,#a9b0b6
                                    7202,7202,#999ea1
                                    7203,7203,#777d84
                                    7204,7204,#73767d
                                    7206,7206,#595d66
                                    7208,7208,#4a4b53
                                    7210,7210,#36363c
                                    7212,7212,#363b41
                                    7214,7214,#232426
                                    7302,7302,#d7c16c
                                    7306,7306,#cca630
                                    7308,7308,#bc962a
                                    7310,7310,#b58926
                                    7402,7402,#e4a371
                                    7406,7406,#be6c3d
                                    7410,7410,#a04431
                                    7500,7500,#d5bcb7
                                    7501,7501,#d9bbb7
                                    7506,7506,#ae5669
                                    7509,7509,#822238
                                    7602,7602,#c1848b
                                    7606,7606,#944751
                                    7802,7802,#c49eaa
                                    7804,7804,#af748f
                                    7902,7902,#bbaeb8
                                    7904,7904,#ab9ca7
                                    7906,7906,#836979
                                    7908,7908,#694d5b
                                    8003,8003,#7570a6
                                    8004,8004,#635992
                                    8102,8102,#7a87a8
                                    8103,8103,#6f799e
                                    8104,8104,#55608d
                                    8105,8105,#495378
                                    8201,8201,#9babc3
                                    8206,8206,#7684af
                                    8207,8207,#3c4476
                                    8208,8208,#495588
                                    8210,8210,#262c5d
                                    8300,8300,#d9dbd7
                                    8302,8302,#b5c1c7
                                    8310,8310,#527498
                                    8312,8312,#45688f
                                    8314,8314,#1f3759
                                    8402,8402,#5794bf
                                    8406,8406,#2563a0
                                    8502,8502,#9bb8a8
                                    8504,8504,#87aa99
                                    8506,8506,#546f5c
                                    8604,8604,#94a980
                                    8606,8606,#798b6c
                                    8702,8702,#a1a171
                                    8704,8704,#838252
                                    8706,8706,#68683c
                                    8802,8802,#6f913a
                                    8806,8806,#566e42
                                    8900,8900,#d0c394
                                    8901,8901,#c6b780
                                    8904,8904,#a79e52
                                    8906,8906,#938a35
                                    9000,9000,#f2decc
                                    9006,9006,#84453e
                                    9008,9008,#74322d
                                    9101,9101,#bca56d
                                    9102,9102,#9c854e
                                    9104,9104,#896e3e
                                    9106,9106,#775e32
                                    9202,9202,#cf897a
                                    9204,9204,#bf7a6d
                                    9206,9206,#ae665c
                                    9207,9207,#f5e4c9
                                    9208,9208,#cd876b
                                    9210,9210,#9f502e
                                    9212,9212,#90482e
                                    9214,9214,#d69986
                                    9216,9216,#9b3832
                                    9218,9218,#8f3d38
                                    9220,9220,#e2767a
                                    9222,9222,#ce575a
                                    9224,9224,#b40032
                                    9226,9226,#c56680
                                    9228,9228,#b73b6d
                                    9230,9230,#701c40
                                    9232,9232,#5f273b
                                    9234,9234,#c97e8a
                                    9236,9236,#b96675
                                    9238,9238,#5f273d
                                    9240,9240,#c66b86
                                    9242,9242,#974655
                                    9244,9244,#a03577
                                    9248,9248,#6a7fa0
                                    9250,9250,#273b54
                                    9252,9252,#272b3b
                                    9254,9254,#8cafc2
                                    9256,9256,#7093af
                                    9258,9258,#40709d
                                    9262,9262,#629fa3
                                    9264,9264,#006b82
                                    9266,9266,#7aa3c2
                                    9268,9268,#0080af
                                    9270,9270,#509881
                                    9272,9272,#008a77
                                    9274,9274,#35816f
                                    9276,9276,#95b2a0
                                    9278,9278,#48725f
                                    9280,9280,#446b5a
                                    9282,9282,#9dada4
                                    9284,9284,#919982
                                    9286,9286,#4e6f62
                                    9288,9288,#384446
                                    9290,9290,#597632
                                    9292,9292,#425432
                                    9294,9294,#dccfa0
                                    9296,9296,#ad9e74
                                    9298,9298,#a48f5d
                                    9300,9300,#626242
                                    9304,9304,#835c44
                                    9306,9306,#6b4c54
                                    9308,9308,#503b35
                                    9310,9310,#a29c8f
                                    9312,9312,#655d54
                                    9314,9314,#524a46";

        public static List<Fabric> Fabrics = new List<Fabric>
        {
                new Fabric("Aida 16", 16, "Blockweave", 1, "100% cotton"),
                new Fabric("Aida 14", 14, "Blockweave", 1, "100% cotton"),
                new Fabric("Aida 18", 18, "Blockweave", 2, "100% cotton"),
                new Fabric("Aida 11", 11, "Blockweave", 3, "100% cotton"),
                new Fabric("Aida 10", 10, "Blockweave", 3, "100% cotton"),
                new Fabric("Linda", 27, "Evenweave", 1, "100% cotton"),
                new Fabric("Jubilee", 28, "Evenweave", 3, "100% cotton"),
                new Fabric("Annabelle", 28, "Evenweave", 3, "100% cotton"),
                new Fabric("Dublin", 20, "Evenweave", 3, "100% linen"),
                new Fabric("Cashel", 28, "Evenweave", 2, "100% linen"),
                new Fabric("Belfast", 32, "Evenweave", 2, "100% linen"),
                new Fabric("Permin", 32, "Evenweave", 2, "100% linen"),
                new Fabric("Edinburgh", 36, "Evenweave", 3, "100% linen"),
                new Fabric("Newcastle", 40, "Evenweave", 3, "100% linen"),
                new Fabric("Lugana", 25, "Evenweave", 1, "52% cotton & 48% rayon"),
                new Fabric("Murano", 32, "Evenweave", 1, "52% cotton & 48% rayon"),
                new Fabric("Bellana", 20, "Evenweave", 3, "52% cotton & 48% rayon"),
                new Fabric("Perlleinen80", 20, "Evenweave", 3, "60% polyester & 40% linen"),
                new Fabric("Perlleinen100", 25, "Evenweave", 3, "60% polyester & 40% linen"),
                new Fabric("Perlleinen", 32, "Evenweave", 3, "52% cotton & 48% rayon"),
                new Fabric("Brittney", 28, "Evenweave", 2, "52% cotton & 48% rayon"),
                new Fabric("Lucan", 32, "Evenweave", 2, "48% cotton & 52% linen")
        };


        public static List<SeedKit> Kits = new List<SeedKit>
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
    }

    public class Fabric
    {
        public string Type { get; }
        public sbyte Count { get; }
        public string Name { get; }
        public sbyte Priority { get; }
        public string Content { get; }

        public Fabric(string name, sbyte count, string type, sbyte priority, string content)
        {
            Type = type;
            Count = count;
            Name = name;
            Priority = priority;
            Content = content;
        }
    }

    public class SeedKit
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
}
