namespace CarShop.Models
{
    public class NovaPost
    {
        Dictionary<string, List<string>>? Departments { get; set; }
        public NovaPost()
        {
            Departments = new Dictionary<string, List<string>>
            {
                { "Kamyanske", new List<string>
                    {
                        "N 1, bul. Nezalezhnosti 2a",
                        "N 2, str. Svobody 13",
                        "N 3, pr. Anoshkina 55",
                        "N 4, bul. Budivelnykiv 63",
                        "N 5, pr. Peremogy 63",
                    } 
                },
                { "Dnipro", new List<string>
                    {
                        "N 1, Stepana Bandery 10/3",
                        "N 2, pr. Gagarina 43",
                        "N 3, Marshala Malinovskogo 114",
                        "N 4, pl. Vokzalna 6",
                        "N 5, Budivelnykiv 13",
                    }
                },
                { "Kyiv", new List<string>
                    {
                        "N 1, pr. Sobornosti 3",
                        "N 2, Verbova 63",
                        "N 3, pl. Svyatoshinska 1",
                        "N 4, pr. Peremohy 7v",
                        "N 5, Khreschatyk 11",
                    }
                },
                { "Kharkiv", new List<string>
                    {
                        "N 1, Heroiv Kharkova 1",
                        "N 2, Chernyshevka 33",
                        "N 3, pr. Gagarina 41/2",
                        "N 4, pr. Peremohy 61",
                        "N 5, Budivelnykiv 53",
                    }
                },
                { "Lviv", new List<string>
                    {
                        "N 1, Bohdana Hmelnitskogo 21",
                        "N 2, Yaroslava Mudrogo 59",
                        "N 3, Volodymyra Velykogo 43",
                        "N 4, pr. Svobody 1",
                        "N 5, Shevchenka 323",
                    }
                },

            };
        }
    }
}
