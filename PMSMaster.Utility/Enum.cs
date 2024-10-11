
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Utility
{
    public enum LeadCategory
    {
        Hot = 1,
        Warm = 2,
        Cold = 3,
        Dead = 4
    }

    public enum QuotationCategory
    {
        New = 0,
        InProcess = 1,
        Converted = 2,
        NotConverted = 3
    }

    public enum ContactMode
    {
        [Description("Telephonic Call")]
        Mobile = 1,
        [Description("Video Call")]
        Online = 2,
        [Description("Physical Meeting")]
        Offline = 3
    }
    public enum ProjectFile
    {
        [Description("Email")]
        Email = 0,
        [Description("MS Team")]
        MSTeam = 1,
        [Description("FMS")]
        FMS = 2
    }
    public enum ClientCategory
    {
        [Description("Induvial")]
        Induvial = 0,
        [Description("Corporate")]
        Corporate = 1,
        [Description("LSP")]
        LSP = 2
    }

    public enum LanguageEnum
    {
        [Description("Afrikaans")]
        Afrikaans = 1,

        [Description("Albanian")]
        Albanian = 2,

        [Description("Amharic")]
        Amharic = 3,

        [Description("Arabic")]
        Arabic = 4,

        [Description("Armenian")]
        Armenian = 5,

        [Description("Assamese")]
        Assamese = 6,

        [Description("Azerbaijani")]
        Azerbaijani = 7,

        [Description("Baluchi")]
        Baluchi = 8,

        [Description("Belorussian")]
        Belorussian = 9,

        [Description("Bengali")]
        Bengali = 10,

        [Description("Bulgarian")]
        Bulgarian = 11,

        [Description("Burmese")]
        Burmese = 12,

        [Description("Catalan")]
        Catalan = 13,

        [Description("Chinese Traditional")]
        ChineseTraditional = 14,

        [Description("Croatian")]
        Croatian = 15,

        [Description("Czech")]
        Czech = 16,

        [Description("Danish")]
        Danish = 17,

        [Description("Dari")]
        Dari = 18,

        [Description("Dutch")]
        Dutch = 19,

        [Description("English")]
        English = 20,

        [Description("Estonian")]
        Estonian = 21,

        [Description("Farsi")]
        Farsi = 22,

        [Description("Finnish")]
        Finnish = 23,

        [Description("Flemish")]
        Flemish = 24,

        [Description("French")]
        French = 25,

        [Description("Georgian")]
        Georgian = 26,

        [Description("German")]
        German = 27,

        [Description("Greek")]
        Greek = 28,

        [Description("Gujarati")]
        Gujarati = 29,

        [Description("Hausa")]
        Hausa = 30,

        [Description("Hebrew")]
        Hebrew = 31,

        [Description("Hindi")]
        Hindi = 32,

        [Description("Hungarian")]
        Hungarian = 33,

        [Description("Icelandic")]
        Icelandic = 34,

        [Description("Indonesian")]
        Indonesian = 35,

        [Description("Irish")]
        Irish = 36,

        [Description("Italian")]
        Italian = 37,

        [Description("Japanese")]
        Japanese = 38,

        [Description("Javanese")]
        Javanese = 39,

        [Description("Kannada")]
        Kannada = 40,

        [Description("Kashmiri")]
        Kashmiri = 41,

        [Description("Kazakh")]
        Kazakh = 42,

        [Description("Khmer")]
        Khmer = 43,

        [Description("Konkini")]
        Konkini = 44,

        [Description("Korean")]
        Korean = 45,

        [Description("Kurmanji")]
        Kurmanji = 46,

        [Description("Lao")]
        Lao = 47,

        [Description("Latvian")]
        Latvian = 48,

        [Description("Lithuanian")]
        Lithuanian = 49,

        [Description("Macedonian")]
        Macedonian = 50,

        [Description("Malagasy")]
        Malagasy = 51,

        [Description("Malay")]
        Malay = 52,

        [Description("Malayalam")]
        Malayalam = 53,

        [Description("Marathi")]
        Marathi = 54,

        [Description("Mongolian")]
        Mongolian = 55,

        [Description("Nepali")]
        Nepali = 56,

        [Description("Norwegian")]
        Norwegian = 57,

        [Description("Oriya")]
        Oriya = 58,

        [Description("Polish")]
        Polish = 59,

        [Description("Persian")]
        Persian = 60,

        [Description("Portuguese Portugal")]
        PortuguesePortugal = 61,

        [Description("Punjabi")]
        Punjabi = 62,

        [Description("Pushtu")]
        Pushtu = 63,

        [Description("Romanian")]
        Romanian = 64,

        [Description("Russian")]
        Russian = 65,

        [Description("Sanskrit")]
        Sanskrit = 66,

        [Description("Serbian")]
        Serbian = 67,

        [Description("Sinhalese")]
        Sinhalese = 68,

        [Description("Slovak")]
        Slovak = 69,

        [Description("Somali")]
        Somali = 70,

        [Description("Spanish Spain")]
        SpanishSpain = 71,

        [Description("Swedish")]
        Swedish = 72,

        [Description("Sudanese")]
        Sudanese = 73,

        [Description("Swahili")]
        Swahili = 74,

        [Description("Tamil")]
        Tamil = 75,

        [Description("Telugu")]
        Telugu = 76,

        [Description("Tibetan")]
        Tibetan = 77,

        [Description("Tigrigna")]
        Tigrigna = 78,

        [Description("Turkish")]
        Turkish = 79,

        [Description("Thai")]
        Thai = 80,

        [Description("Ukrainian")]
        Ukrainian = 81,

        [Description("Urdu")]
        Urdu = 82,

        [Description("Vietnamese")]
        Vietnamese = 83,

        [Description("Tagalog")]
        Tagalog = 84,

        [Description("Sorani")]
        Sorani = 85,

        [Description("Kurdish")]
        Kurdish = 86,

        [Description("Sindhi")]
        Sindhi = 87,

        [Description("Maltese")]
        Maltese = 88,

        [Description("Portuguese Brazil")]
        PortugueseBrazil = 89,

        [Description("Spanish Argentina")]
        SpanishArgentina = 90,

        [Description("Spanish Chile")]
        SpanishChile = 91,

        [Description("Spanish Mexico")]
        SpanishMexico = 92,

        [Description("Chinese Simplified")]
        ChineseSimplified = 93,

        [Description("Sylheti")]
        Sylheti = 94,

        [Description("Manipuri")]
        Manipuri = 95,

        [Description("Bengali Bangladesi")]
        BengaliBangladesi = 96,

        [Description("Gaelic")]
        Gaelic = 97,

        [Description("Combodian")]
        Combodian = 98,

        [Description("Tajik")]
        Tajik = 99,

        [Description("Xhosa")]
        Xhosa = 100,

        [Description("Uzbek")]
        Uzbek = 101,

        [Description("Slovanian")]
        Slovanian = 102,

        [Description("Lingala")]
        Lingala = 103,

        [Description("Mirpuri")]
        Mirpuri = 104,

        [Description("Zulu")]
        Zulu = 105,

        [Description("Berber")]
        Berber = 106,

        [Description("Welsh")]
        Welsh = 107,

        [Description("Igbo")]
        Igbo = 108,

        [Description("Yoruba")]
        Yoruba = 109,

        [Description("Malagasy Domestic")]
        MalagasyDomestic = 110,

        [Description("Taiwanese")]
        Taiwanese = 111,

        [Description("Shona")]
        Shona = 112,

        [Description("Sesotho")]
        Sesotho = 113,

        [Description("Wolof")]
        Wolof = 114,

        [Description("Rajasthani")]
        Rajasthani = 115,

        [Description("Latin")]
        Latin = 116,

        [Description("Kirgiz")]
        Kirgiz = 117,

        [Description("Turkmen")]
        Turkmen = 118,

        [Description("Philippines")]
        Philippines = 119,

        [Description("Portuguese")]
        Portuguese = 120,

        [Description("Spanish")]
        Spanish = 121,

        [Description("Hmong")]
        Hmong = 122,

        [Description("Haryanvi")]
        Haryanvi = 123,

        [Description("Pakistani Punjabi")]
        PakistaniPunjabi = 124,

        [Description("Bhutanese")]
        Bhutanese = 125,

        [Description("Canadian French")]
        CanadianFrench = 126,

        [Description("South american spainsh")]
        SouthAmericanSpanish = 127,

        [Description("European spainsh")]
        EuropeanSpanish = 128,

        [Description("south american spanish")]
        SouthAmericanSpanish2 = 129,

        [Description("chinese mandrine")]
        ChineseMandrine = 130,

        [Description("Bhojpuri")]
        Bhojpuri = 131,

        [Description("Algerian")]
        Algerian = 132,

        [Description("Marwadi")]
        Marwadi = 133,

        [Description("Bosnian")]
        Bosnian = 134,

        [Description("Ethopian")]
        Ethopian = 135,

        [Description("Karen")]
        Karen = 136,

        [Description("Spanish Latin")]
        SpanishLatin = 137,

        [Description("Chibcha")]
        Chibcha = 138,

        [Description("Maithili")]
        Maithili = 139,

        [Description("Different Languages")]
        DifferentLanguages = 140,

        [Description("Kuwait")]
        Kuwait = 141,

        [Description("Anzeri")]
        Anzeri = 142,

        [Description("Karenni")]
        Karenni = 143,

        [Description("Chinese Honkong")]
        ChineseHonkong = 144,

        [Description("Khashi")]
        Khashi = 145,

        [Description("Kuwaiti Arabic")]
        KuwaitiArabic = 146,

        [Description("Mexican")]
        Mexican = 147,

        [Description("Mizo")]
        Mizo = 148,

        [Description("Chin")]
        Chin = 149,

        [Description("Sotho")]
        Sotho = 150,

        [Description("Moldovian")]
        Moldovian = 151,

        [Description("Srilankan Tamil")]
        SrilankanTamil = 152,

        [Description("Dogri")]
        Dogri = 153,

        [Description("Chuukese")]
        Chuukese = 154,

        [Description("Bahasa-Malaysia")]
        BahasaMalaysia = 155,

        [Description("Dutch-Belgium")]
        DutchBelgium = 156,

        [Description("German-Switzerland")]
        GermanSwitzerland = 157,

        [Description("Bahasa-Indonesia")]
        BahasaIndonesia = 158,

        [Description("Algerian-French")]
        AlgerianFrench = 159,

        [Description("Hiligaynon")]
        Hiligaynon = 160,

        [Description("Dhivehi")]
        Dhivehi = 161,

        [Description("Cebuano")]
        Cebuano = 162,

        [Description("Malaysian")]
        Malaysian = 163,

        [Description("Nagamese")]
        Nagamese = 164,

        [Description("Tulu")]
        Tulu = 165,

        [Description("Assyrian")]
        Assyrian = 166,

        [Description("Kirundi")]
        Kirundi = 167,

        [Description("Garo")]
        Garo = 168,

        [Description("Dutch-Netherland")]
        DutchNetherland = 169,

        [Description("Danish-Norway")]
        DanishNorway = 170,

        [Description("Moroccan Arabic")]
        MoroccanArabic = 171,

        [Description("Bodo")]
        Bodo = 172,

        [Description("Santali")]
        Santali = 173,

        [Description("Cantonese")]
        Cantonese = 174,

        [Description("Tetum")]
        Tetum = 175,

        [Description("Oshiwambo")]
        Oshiwambo = 176,

        [Description("Montenegrin")]
        Montenegrin = 177,

        [Description("Canadian English")]
        CanadianEnglish = 178,

        [Description("Kinyarwanda")]
        Kinyarwanda = 179,

        [Description("British Sign Language")]
        BritishSignLanguage = 180,

        [Description("European French")]
        EuropeanFrench = 181,

        [Description("Khasi")]
        Khasi = 182,

        [Description("Hindko")]
        Hindko = 183,

        [Description("Spanish Castilian")]
        SpanishCastilian = 184,

        [Description("Yoruba Domestic")]
        YorubaDomestic = 185,

        [Description("Fulani")]
        Fulani = 186,

        [Description("Basquee")]
        Basquee = 187,

        [Description("Ndebele")]
        Ndebele = 188,

        [Description("Galician (Galician)")]
        GalicianGalician = 189,

        [Description("Catalan (Andorra)")]
        CatalanAndorra = 190,

        [Description("Catalan (Catalan)")]
        CatalanCatalan = 191,

        [Description("Chinese Cantonese")]
        ChineseCantonese = 192,

        [Description("Fijian")]
        Fijian = 193,

        [Description("Tongan")]
        Tongan = 194,

        [Description("Chinese")]
        Chinese = 195,

        [Description("Valencia")]
        Valencia = 196,

        [Description("Akan")]
        Akan = 197,

        [Description("Oromo")]
        Oromo = 198,

        [Description("Samoan")]
        Samoan = 199,

        [Description("Norwegian Norway")]
        NorwegianNorway = 200,

        [Description("Norwegian Bokmal")]
        NorwegianBokmal = 201,

        [Description("English To Dzongkha (Bhutan)")]
        EnglishToDzongkha = 202,

        [Description("English To Dhivehi (Maldives)")]
        EnglishToDhivehi = 203,

        [Description("Dinka")]
        Dinka = 204,

        [Description("UK English")]
        UKEnglish = 205,

        [Description("Tswana")]
        Tswana = 206,

        [Description("Pakistani Urdu")]
        PakistaniUrdu = 207,

        [Description("Singaporean Chinese")]
        SingaporeanChinese = 208,

        [Description("Sudanese Arabic")]
        SudaneseArabic = 209,

        [Description("Pilipino")]
        Pilipino = 210,

        [Description("Egyptian")]
        Egyptian = 211,

        [Description("UP Hindi")]
        UPHindi = 212,

        [Description("MP Hindi")]
        MPHindi = 213,

        [Description("RJ Hindi")]
        RJHindi = 214,

        [Description("Tamil (Malaysian)")]
        TamilMalaysian = 215,

        [Description("Maldivian")]
        Maldivian = 216,

        [Description("Meitei (Assamese)")]
        MeiteiAssamese = 217,

        [Description("Uighur")]
        Uighur = 218,

        [Description("Quecha")]
        Quecha = 219,

        [Description("Siraiki")]
        Siraiki = 220,

        [Description("Ewe")]
        Ewe = 221,

        [Description("Twi")]
        Twi = 222,

        [Description("Afar")]
        Afar = 223,

        [Description("Faroese")]
        Faroese = 224,

        [Description("Klingon")]
        Klingon = 225,

        [Description("Otomian")]
        Otomian = 226,

        [Description("Nyishi")]
        Nyishi = 227,

        [Description("Kinnauri")]
        Kinnauri = 228,
    }

    public enum Month
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    public enum Year
    {
        [Description("2020")]
        Year2020 = 2020,
        [Description("2021")]
        Year2021,
        [Description("2022")]
        Year2022,
        [Description("2023")]
        Year2023,
        [Description("2024")]
        Year2024,
        [Description("2025")]
        Year2025,
        [Description("2026")]
        Year2026,
        [Description("2027")]
        Year2027,
        [Description("2028")]
        Year2028,
        [Description("2029")]
        Year2029,
        [Description("2030")]
        Year2030
    }



}
