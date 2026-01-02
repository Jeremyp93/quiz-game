using QuizGame.Domain.Entities;
using QuizGame.Domain.Enums;

namespace QuizGame.Infrastructure.Data;

public static class DbSeeder
{
    public static void SeedDatabase(QuizGameDbContext context)
    {
        // Clear existing data
        context.Questions.RemoveRange(context.Questions);
        context.Themes.RemoveRange(context.Themes);
        context.SaveChanges();

        // ==================== THEMES ====================
        var themeJonathan = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "jonathan",
            NameFr = "Jonathan",
            NameNl = "Jonathan",
            IsActive = true,
            SortOrder = 1
        };

        var themePieterkeshoeve = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "pieterkeshoeve",
            NameFr = "Pieterkeshoeve",
            NameNl = "Pieterkeshoeve",
            IsActive = true,
            SortOrder = 2
        };

        var themeGeography = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "geography",
            NameFr = "Géographie",
            NameNl = "Aardrijkskunde",
            IsActive = true,
            SortOrder = 3
        };

        var themeGastronomy = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "gastronomy",
            NameFr = "Gastronomie",
            NameNl = "Gastronomie",
            IsActive = true,
            SortOrder = 4
        };

        var themeAnimals = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "animals",
            NameFr = "Animaux",
            NameNl = "Dieren",
            IsActive = true,
            SortOrder = 5
        };

        var themeTechnology = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "technology",
            NameFr = "Technologie",
            NameNl = "Technologie",
            IsActive = true,
            SortOrder = 6
        };

        var themeCycling = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "cycling",
            NameFr = "Cyclisme",
            NameNl = "Wielrennen",
            IsActive = true,
            SortOrder = 7
        };

        var themeSurprise = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "surprise",
            NameFr = "Surprise",
            NameNl = "Verassing",
            IsActive = true,
            SortOrder = 8
        };

        context.Themes.AddRange([
            themeJonathan, themePieterkeshoeve, themeTechnology, themeGastronomy,
            themeGeography, themeCycling, themeSurprise, themeAnimals
        ]);
        context.SaveChanges();

        var questions = new List<Question>
        {
            // ==================== MCQ QUESTIONS ====================

            // Jonathan
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Jonathan",
                Tags = "birthday",
                TextFr = "Quel est la date d'anniversaire de Jonathan ?",
                TextNl = "Wat is de geboortedatum van Jonathan?",
                ThemeId = themeJonathan.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "15",
                    ChoiceANl = "15",
                    ChoiceBFr = "17",
                    ChoiceBNl = "17",
                    ChoiceCFr = "28",
                    ChoiceCNl = "28",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Jonathan",
                Tags = "fiancee,wedding",
                TextFr = "En quelle année Jonathan a-t-il fait sa demande en mariage à Lies ?",
                TextNl = "In welk jaar heeft Jonathan aan Lies ten huwelijk gevraagd?",
                ThemeId = themeJonathan.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "2021",
                    ChoiceANl = "2021",
                    ChoiceBFr = "2022",
                    ChoiceBNl = "2022",
                    ChoiceCFr = "2023",
                    ChoiceCNl = "2023",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Jonathan",
                Tags = "fiancee,wedding",
                TextFr = "Dans quelle école Jonathan n'a-t-il jamais été scolarisé ?",
                TextNl = "Op welke school is Jonathan nooit naar school gegaan?",
                ThemeId = themeJonathan.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "KASPW",
                    ChoiceANl = "KASPW",
                    ChoiceBFr = "KAE",
                    ChoiceBNl = "KAE",
                    ChoiceCFr = "LCO",
                    ChoiceCNl = "LCO",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Jonathan",
                Tags = "fiancee,wedding",
                TextFr = "Combien d'amis à Jonathan sur Facebook ?",
                TextNl = "Hoeveel vrienden heeft Jonathan op Facebook?",
                ThemeId = themeJonathan.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "180",
                    ChoiceANl = "180",
                    ChoiceBFr = "250",
                    ChoiceBNl = "250",
                    ChoiceCFr = "280",
                    ChoiceCNl = "280",
                    CorrectChoice = McqChoice.C
                }
            },
            // Pieterkeshoeve
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Pieterkeshoeve",
                Tags = "name,animal",
                TextFr = "Quel prénom d'animal n'est pas présent au Pieterkeshoeve ?",
                TextNl = "Welke dierennaam is niet aanwezig op Pieterkeshoeve?",
                ThemeId = themePieterkeshoeve.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Fiona",
                    ChoiceANl = "Fiona",
                    ChoiceBFr = "Pipa",
                    ChoiceBNl = "Pipa",
                    ChoiceCFr = "Maïa",
                    ChoiceCNl = "Maïa",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Pieterkeshoeve",
                Tags = "website,names",
                TextFr = "Combien de familles sont répertoriées sur le site du Pieterkeshoeve ?",
                TextNl = "Hoeveel families worden er vermeld op de Pieterkeshoeve-website?",
                ThemeId = themePieterkeshoeve.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "8",
                    ChoiceANl = "8",
                    ChoiceBFr = "10",
                    ChoiceBNl = "10",
                    ChoiceCFr = "16",
                    ChoiceCNl = "16",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Pieterkeshoeve",
                Tags = "habitants,number",
                TextFr = "Combien d'habitants y-a-t-il plus ou moins à Vechmaal ?",
                TextNl = "Hoeveel inwoners zijn er ongeveer in Vechmaal?",
                ThemeId = themePieterkeshoeve.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "400",
                    ChoiceANl = "400",
                    ChoiceBFr = "500",
                    ChoiceBNl = "500",
                    ChoiceCFr = "600",
                    ChoiceCNl = "600",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Pieterkeshoeve",
                Tags = "agriculture",
                TextFr = "Quel est le principal facteur qui influence le rendement en sucre de la betterave sucrière, et qui nécessite un suivi précis lors de sa culture ?",
                TextNl = "Wat is de belangrijkste factor die de suikeropbrengst van suikerbiet beïnvloedt en die nauwlettend gevolgd moet worden tijdens de teelt?",
                ThemeId = themePieterkeshoeve.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Le pH du sol",
                    ChoiceANl = "De pH van de grond",
                    ChoiceBFr = "Le taux d'humidité du sol",
                    ChoiceBNl = "De luchtvochtigheid van de grond",
                    ChoiceCFr = "La température moyenne annuelle",
                    ChoiceCNl = "De jaarlijkse gemiddelde temperatuur",
                    CorrectChoice = McqChoice.A
                }
            },
            // Geogrpahy
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Geography",
                Tags = "capitals",
                TextFr = "Quel est la capitale du Canada ?",
                TextNl = "Wat is de hoofdstad van Canada?",
                ThemeId = themeGeography.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Ottawa",
                    ChoiceANl = "Ottawa",
                    ChoiceBFr = "Montreal",
                    ChoiceBNl = "Montreal",
                    ChoiceCFr = "Quebec",
                    ChoiceCNl = "Quebec",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Geography",
                Tags = "habitants",
                TextFr = "Comment s'appelle les habitants de Rio de Janeiro ?",
                TextNl = "Hoe worden de inwoners van Rio de Janeiro genoemd?",
                ThemeId = themeGeography.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Les riohas",
                    ChoiceANl = "De riohas",
                    ChoiceBFr = "Les paulistas",
                    ChoiceBNl = "De paulistas",
                    ChoiceCFr = "Les cariocas",
                    ChoiceCNl = "De cariocas",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Geography",
                Tags = "habitants",
                TextFr = "Classe, par ordre décroissant les 4 pays les plus peuplés du monde ?",
                TextNl = "Rang de de grootste tot de kleinste de 4 meest bevolkte landen ter wereld?",
                ThemeId = themeGeography.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Inde, Chine, USA, Indonésie",
                    ChoiceANl = "India, China, USA, Indonesië",
                    ChoiceBFr = "Japon, Inde, Chine, USA",
                    ChoiceBNl = "Japan, India, China, USA",
                    ChoiceCFr = "Chine, Inde, USA, Indonésie",
                    ChoiceCNl = "China, India, USA, Indonesia",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Geography",
                Tags = "islands",
                TextFr = "Quel pays possède le plus grand nombre d’îles au monde ?",
                TextNl = "Welk land heeft het grootste aantal eilanden ter wereld?",
                ThemeId = themeGeography.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Indonésie",
                    ChoiceANl = "Indonesië",
                    ChoiceBFr = "Suède",
                    ChoiceBNl = "Zweden",
                    ChoiceCFr = "Japon",
                    ChoiceCNl = "Japan",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Gastronomy",
                Tags = "chocolat",
                TextFr = "Quel chocolat est le moins gras ?",
                TextNl = "Welke chocolade bevat de minste vet?",
                ThemeId = themeGastronomy.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Chocolat noir",
                    ChoiceANl = "Pure chocolade",
                    ChoiceBFr = "Chocolat au lait",
                    ChoiceBNl = "Melkchocolade",
                    ChoiceCFr = "Chocolat blanc",
                    ChoiceCNl = "Witte chocolade",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Gastronomy",
                Tags = "michelin",
                TextFr = "Quel est le nombres d'étoiles attribuable à un restaurant gastronomique ?",
                TextNl = "Hoeveel sterren kan een gastronomisch restaurant krijgen?",
                ThemeId = themeGastronomy.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "2",
                    ChoiceANl = "2",
                    ChoiceBFr = "3",
                    ChoiceBNl = "3",
                    ChoiceCFr = "4",
                    ChoiceCNl = "4",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Gastronomy",
                Tags = "asperegus",
                TextFr = "Quel pays d'Amérique du Sud est l'un des plus gros producteurs mondiaux d'asperges ?",
                TextNl = "Welk Zuid-Amerikaans land is een van de grootste aspergeproducenten ter wereld?",
                ThemeId = themeGastronomy.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Pérou",
                    ChoiceANl = "Peru",
                    ChoiceBFr = "Bolivie",
                    ChoiceBNl = "Bolivië",
                    ChoiceCFr = "Argentine",
                    ChoiceCNl = "Argentinië",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Gastronomy",
                Tags = "asperegus",
                TextFr = "Qu'est ce que la badiane ?",
                TextNl = "Wat is steranijs?",
                ThemeId = themeGastronomy.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Fruit",
                    ChoiceANl = "Fruit",
                    ChoiceBFr = "Epice",
                    ChoiceBNl = "Kruid",
                    ChoiceCFr = "Liqueur",
                    ChoiceCNl = "Likeur",
                    CorrectChoice = McqChoice.A
                }
            },
            // Animals
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Animals",
                TextFr = "Quel est l'animal terrestre le plus rapide ?",
                TextNl = "Wat is het snelste landdier?",
                Tags = "fastest,cat",
                ThemeId = themeAnimals.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Le guépard",
                    ChoiceANl = "De cheetah",
                    ChoiceBFr = "L'autruche",
                    ChoiceBNl = "De struisvogel",
                    ChoiceCFr = "Le lévrier",
                    ChoiceCNl = "De greyhound",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Animals",
                TextFr = "Lequel de ces animaux n'appartient pas à l'astrologie chinoise ?",
                TextNl = "Hoeveel poten heeft een spin?",
                Tags = "china",
                ThemeId = themeAnimals.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Dragon",
                    ChoiceANl = "Draak",
                    ChoiceBFr = "Chèvre",
                    ChoiceBNl = "Geit",
                    ChoiceCFr = "Chat",
                    ChoiceCNl = "Kat",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Animals",
                Tags = "fly",
                TextFr = "Quel est le seul mammifère capable de voler ?",
                TextNl = "Wat is het enige zoogdier dat kan vliegen?",
                ThemeId = themeAnimals.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "L'écureuil volant",
                    ChoiceANl = "Vliegende eekhoorn",
                    ChoiceBFr = "La chauve-souris",
                    ChoiceBNl = "Vleermuis",
                    ChoiceCFr = "Le poisson volant",
                    ChoiceCNl = "Vliegende vis",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Animals",
                TextFr = "Quel est l'animal le plus venimeux au monde ?",
                TextNl = "Wat is het giftigste dier ter wereld?",
                Tags = "poison",
                ThemeId = themeAnimals.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "La méduse-boîte",
                    ChoiceANl = "Dooskwal",
                    ChoiceBFr = "Le taipan du désert",
                    ChoiceBNl = "Woestijntaipan",
                    ChoiceCFr = "La grenouille dorée",
                    ChoiceCNl = "Gouden gifkikker",
                    CorrectChoice = McqChoice.A
                }
            },
            // Tehcnology
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Tehcnology",
                TextFr = "Que signifie WWW dans une adresse internet ?",
                TextNl = "Wat betekent WWW in een internetadres?",
                Tags = "smarthphones",
                ThemeId = themeTechnology.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "World Wide Web",
                    ChoiceANl = "World Wide Web",
                    ChoiceBFr = "World Web Wide",
                    ChoiceBNl = "World Web Wide",
                    ChoiceCFr = "Wide World Web",
                    ChoiceCNl = "Wide World Web",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Tehcnology",
                TextFr = "Quel composant est considéré comme le « cerveau » de l'ordinateur ?",
                TextNl = "Welke component wordt beschouwd als de « brain » van de computer?",
                Tags = "computer",
                ThemeId = themeTechnology.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Le disque dur",
                    ChoiceANl = "De harde schijf",
                    ChoiceBFr = "La souris",
                    ChoiceBNl = "De muis",
                    ChoiceCFr = "Le processeur",
                    ChoiceCNl = "De processor",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Tehcnology",
                TextFr = "Combien de smartphones se sont vendus dans le monde en 2019 ?",
                TextNl = "Hoeveel smartphones zijn er wereldwijd verkocht in 2019?",
                Tags = "smarthphones",
                ThemeId = themeTechnology.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "7 par seconde",
                    ChoiceANl = "7 per seconde",
                    ChoiceBFr = "18 par seconde",
                    ChoiceBNl = "18 per seconde",
                    ChoiceCFr = "48 par seconde",
                    ChoiceCNl = "48 per seconde",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Tehcnology",
                TextFr = "Aujourd'hui, tu as déjà surement utilisé une clef USB, mais sais-tu ce que signifie USB ?",
                TextNl = "Je hebt waarschijnlijk al een USB-stick gebruikt, maar weet je wat USB betekent?",
                Tags = "usb",
                ThemeId = themeTechnology.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Use System Bluetooth",
                    ChoiceANl = "Use System Bluetooth",
                    ChoiceBFr = "User Server Boot",
                    ChoiceBNl = "User Server Boot",
                    ChoiceCFr = "Universal Serial Bus",
                    ChoiceCNl = "Universal Serial Bus",
                    CorrectChoice = McqChoice.C
                }
            },
            // Cycling
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Cycling",
                TextFr = "Combien de roues a un tricycle ?",
                TextNl = "Hoeveel wielen heeft een driewieler?",
                Tags = "bike",
                ThemeId = themeCycling.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "3",
                    ChoiceANl = "3",
                    ChoiceBFr = "4",
                    ChoiceBNl = "4",
                    ChoiceCFr = "5",
                    ChoiceCNl = "5",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Cycling",
                TextFr = "Quel grand événement cycliste a lieu chaque année en Italie ?",
                TextNl = "Welk groot wielerevenement vindt elk jaar in Italië plaats?",
                Tags = "race",
                ThemeId = themeCycling.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "La Vuelta",
                    ChoiceANl = "De Vuelta",
                    ChoiceBFr = "Le Giro",
                    ChoiceBNl = "De Giro",
                    ChoiceCFr = "La Volta",
                    ChoiceCNl = "De Volta",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Cycling",
                TextFr = "Combien de fois Eddy Merckx a-t-il remporté le Tour de France ?",
                TextNl = "Hoeveel keer heeft Eddy Merckx de Tour de France gewonnen?",
                Tags = "race",
                ThemeId = themeCycling.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "5",
                    ChoiceANl = "5",
                    ChoiceBFr = "7",
                    ChoiceBNl = "7",
                    ChoiceCFr = "9",
                    ChoiceCNl = "9",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Cycling",
                TextFr = "Laquelle de ces équipes cyclistes n'existe pas ?",
                TextNl = "Welke van deze wielerploegen bestaat niet?",
                Tags = "race",
                ThemeId = themeCycling.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "CycloDragon",
                    ChoiceANl = "CycloDragon",
                    ChoiceBFr = "Alpecin-Fenix",
                    ChoiceBNl = "Alpecin-Fenix",
                    ChoiceCFr = "Team Jumbo-Visma",
                    ChoiceCNl = "Team Jumbo-Visma",
                    CorrectChoice = McqChoice.A
                }
            },
            // Surprise
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Surprise",
                TextFr = "Quel est le record du monde pour le plus grand nombre de chaussettes portées en une seule fois ?",
                TextNl = "Wat is het wereldrecord voor het meeste aantal sokken die tegelijkertijd gedragen worden?",
                Tags = "socks",
                ThemeId = themeSurprise.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "399",
                    ChoiceANl = "399",
                    ChoiceBFr = "512",
                    ChoiceBNl = "512",
                    ChoiceCFr = "717",
                    ChoiceCNl = "717",
                    CorrectChoice = McqChoice.C
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 1,
                IsActive = true,
                IsPriority = true,
                Category = "Surprise",
                TextFr = "Quel est le mois où naissent le plus de bébés dans le monde en général ?",
                TextNl = "In welke maand worden wereldwijd de meeste baby's geboren?",
                Tags = "birth",
                ThemeId = themeSurprise.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "Janvier",
                    ChoiceANl = "Januari",
                    ChoiceBFr = "Septembre",
                    ChoiceBNl = "September",
                    ChoiceCFr = "Decembre",
                    ChoiceCNl = "December",
                    CorrectChoice = McqChoice.B
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 2,
                IsActive = true,
                IsPriority = true,
                Category = "Surprise",
                TextFr = "Laquelle de ces affirmations est fausse ?",
                TextNl = "Welke van deze beweringen is fout?",
                Tags = "false",
                ThemeId = themeSurprise.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "La muraille de Chine est visible depuis la lune",
                    ChoiceANl = "De Chinese muur is zichtbaar vanaf de maan",
                    ChoiceBFr = "Il n'existe aucun aliment naturellement bleu",
                    ChoiceBNl = "Er bestaat geen natuurlijk blauw voedsel",
                    ChoiceCFr = "Une des gargouilles de la cathédrale de Washington représente Dark Vador",
                    ChoiceCNl = "Een van de gargouilles van de kathedraal van Washington stelt Darth Vader voor",
                    CorrectChoice = McqChoice.A
                }
            },
            new() {
                Id = Guid.NewGuid(),
                Type = QuestionType.Mcq,
                Difficulty = 3,
                IsActive = true,
                IsPriority = true,
                Category = "Surprise",
                TextFr = "Classe les 4 pourcentages suivants du plus grand au plus petit : le % de SPAMS dans les mails échangés sur internet, Le % de bougies achetées par des femmes, le % de GPS achetés par des hommes, le % de gens mourant à l'hôpital",
                TextNl = "Orden de 4 procenten van groot naar klein: het % van SPAM in de e-mails die op internet worden uitgewisseld, het % van kaarsen gekocht door vrouwen, het % van GPS gekocht door mannen, het % van mensen die in het ziekenhuis overlijden",
                Tags = "order",
                ThemeId = themeSurprise.Id,
                McqDetails = new McqQuestionDetails
                {
                    ChoiceAFr = "A>B>C>D",
                    ChoiceANl = "A>B>C>D",
                    ChoiceBFr = "C>A>B>D",
                    ChoiceBNl = "C>A>B>D",
                    ChoiceCFr = "B>C>A>D",
                    ChoiceCNl = "B>C>A>D",
                    CorrectChoice = McqChoice.B
                }
            },
        };

        // ==================== PHASE 1 REGULAR QUESTIONS (40 questions) ====================
        // Mix: Simple, Medium (more), Few Hard - Various topics for Belgian families (30-60 years)

        // SIMPLE - Phase 1 (10 questions)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "belgium,cities",
            TextFr = "Quelle est la capitale des Etats-Unis ?",
            TextNl = "Wat is de hoofdstad van Amerika?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Washington",
                AnswerNl = "Washington"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "instruments",
            TextFr = "Combien de touches blanches y a-t-il sur un piano standard ?",
            TextNl = "Hoeveel witte toetsen heeft een standaard piano?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "52",
                AnswerNl = "52"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "General",
            Tags = "time",
            TextFr = "Combien de secondes y a-t-il dans une heure ?",
            TextNl = "Hoeveel seconden heeft een uur?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "3 600",
                AnswerNl = "3 600"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Food",
            Tags = "fruits",
            TextFr = "De quelle couleur est une orange mûre ?",
            TextNl = "Welke kleur heeft een rijpe sinaasappel?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Orange",
                AnswerNl = "Oranje"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Nature",
            Tags = "animals",
            TextFr = "Quel animal produit du miel ?",
            TextNl = "Welk dier maakt honing?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Abeille",
                AnswerNl = "Bij"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "europe,countries",
            TextFr = "Quelle est la capitale de l'Allemagne ?",
            TextNl = "Wat is de hoofdstad van Duitsland?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Berlin",
                AnswerNl = "Berlijn"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Sports",
            Tags = "football",
            TextFr = "Combien de joueurs y a-t-il sur le terrain dans une équipe de basket ?",
            TextNl = "Hoeveel spelers staan er op het veld in een basketbalteam?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "5",
                AnswerNl = "5"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "General",
            Tags = "colors",
            TextFr = "Quelle couleur obtient-on en mélangeant du bleu et du jaune ?",
            TextNl = "Welke kleur krijg je door blauw en geel te mengen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Vert",
                AnswerNl = "Groen"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Science",
            Tags = "temperature",
            TextFr = "À quelle température l'eau bout-elle en degrés Celsius ?",
            TextNl = "Bij welke temperatuur kookt water in graden Celsius?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "100",
                AnswerNl = "100"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Culture",
            Tags = "disney",
            TextFr = "Comment s'appelle le jouet cowboy dans 'Toy Story' ?",
            TextNl = "Hoe heet de cowboypop in 'Toy Story'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Woody",
                AnswerNl = "Woody"
            }
        });

        // MEDIUM - Phase 1 (25 questions)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "world-history,dates",
            TextFr = "En quelle année l'homme a-t-il marché sur la Lune ?",
            TextNl = "In welk jaar liep de mens op de Maan?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1969",
                AnswerNl = "1969"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "rivers,world",
            TextFr = "Quel fleuve traverse Paris ?",
            TextNl = "Welke rivier stroomt door Parijs?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "La Seine",
                AnswerNl = "De Seine"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "pop,singers",
            TextFr = "Qui est surnommé 'Le Roi de la Pop' ?",
            TextNl = "Wie wordt de 'Koning van de Pop' genoemd?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Michael Jackson",
                AnswerNl = "Michael Jackson"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Literature",
            Tags = "comics,belgium",
            TextFr = "Qui est le créateur de Tintin ?",
            TextNl = "Wie is de bedenker van Kuifje?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Hergé",
                AnswerNl = "Hergé"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Science",
            Tags = "planets",
            TextFr = "Quelle est la plus grande planète du système solaire ?",
            TextNl = "Wat is de grootste planeet in ons zonnestelsel?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Jupiter",
                AnswerNl = "Jupiter"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "world-history",
            TextFr = "Qui a peint la chapelle Sixtine ?",
            TextNl = "Wie schilderde de Sixtijnse Kapel?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Michel-Ange",
                AnswerNl = "Michelangelo"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Sports",
            Tags = "football,world",
            TextFr = "Dans quel pays se déroule la Premier League ?",
            TextNl = "In welk land vindt de Premier League plaats?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Angleterre",
                AnswerNl = "Engeland"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "world,mountains",
            TextFr = "Quel est le plus haut sommet du monde ?",
            TextNl = "Wat is de hoogste berg ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Everest",
                AnswerNl = "Mount Everest"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "cinema",
            TextFr = "Qui a réalisé le film 'Titanic' ?",
            TextNl = "Wie regisseerde de film 'Titanic'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "James Cameron",
                AnswerNl = "James Cameron"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Science",
            Tags = "body",
            TextFr = "Quel est l'os le plus long du corps humain ?",
            TextNl = "Wat is het langste bot in het menselijk lichaam?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Fémur",
                AnswerNl = "Dijbeen"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Culture",
            Tags = "monuments,world",
            TextFr = "Dans quelle ville se trouve la Statue de la Liberté ?",
            TextNl = "In welke stad staat het Vrijheidsbeeld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "New York",
                AnswerNl = "New York"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "egypt",
            TextFr = "Comment s'appelle le célèbre monument funéraire égyptien ?",
            TextNl = "Hoe heet het beroemde Egyptische grafmonument?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pyramide",
                AnswerNl = "Piramide"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "classical",
            TextFr = "Qui a composé 'Les Quatre Saisons' ?",
            TextNl = "Wie componeerde 'De Vier Jaargetijden'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Vivaldi",
                AnswerNl = "Vivaldi"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "europe",
            TextFr = "Quelle est la capitale de l'Espagne ?",
            TextNl = "Wat is de hoofdstad van Spanje?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Madrid",
                AnswerNl = "Madrid"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Literature",
            Tags = "authors",
            TextFr = "Qui a écrit 'Roméo et Juliette' ?",
            TextNl = "Wie schreef 'Romeo en Julia'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "William Shakespeare",
                AnswerNl = "William Shakespeare"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Technology",
            Tags = "companies",
            TextFr = "Qui a fondé Microsoft ?",
            TextNl = "Wie heeft Microsoft opgericht?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Bill Gates",
                AnswerNl = "Bill Gates"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Nature",
            Tags = "animals,ocean",
            TextFr = "Quel est le plus grand mammifère marin ?",
            TextNl = "Wat is het grootste zeezoogdier?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Baleine bleue",
                AnswerNl = "Blauwe vinvis"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Sports",
            Tags = "tennis",
            TextFr = "Combien de fois Rafael Nadal a-t-il remporté Roland-Garros ?",
            TextNl = "Hoeveel keer won Rafael Nadal Roland-Garros?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "14",
                AnswerNl = "14"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "oceans,world",
            TextFr = "Quel est le plus grand océan du monde ?",
            TextNl = "Wat is de grootste oceaan ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pacifique",
                AnswerNl = "Stille Oceaan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Food",
            Tags = "cuisine,world",
            TextFr = "De quel pays vient le sushi ?",
            TextNl = "Uit welk land komt sushi?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Japon",
                AnswerNl = "Japan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "world-history",
            TextFr = "En quelle année est tombé le Mur de Berlin ?",
            TextNl = "In welk jaar viel de Berlijnse Muur?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1989",
                AnswerNl = "1989"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Science",
            Tags = "chemistry",
            TextFr = "Quel est le symbole chimique de l'or ?",
            TextNl = "Wat is het chemisch symbool voor goud?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Au",
                AnswerNl = "Au"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "actors",
            TextFr = "Quel acteur a joué Jack Sparrow dans 'Pirates des Caraïbes' ?",
            TextNl = "Welke acteur speelde Jack Sparrow in 'Pirates of the Caribbean'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Johnny Depp",
                AnswerNl = "Johnny Depp"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Culture",
            Tags = "festivals,music",
            TextFr = "Dans quel pays se déroule le festival de musique Coachella ?",
            TextNl = "In welk land vindt het muziekfestival Coachella plaats?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "États-Unis",
                AnswerNl = "Verenigde Staten"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "world,rivers",
            TextFr = "Quel est le plus long fleuve du monde ?",
            TextNl = "Wat is de langste rivier ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Nil",
                AnswerNl = "Nijl"
            }
        });

        // HARD - Phase 1 (5 questions)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "History",
            Tags = "world-history,ancient",
            TextFr = "Qui a construit les pyramides d'Égypte ?",
            TextNl = "Wie bouwde de piramides van Egypte?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Égyptiens",
                AnswerNl = "Egyptenaren"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "Science",
            Tags = "physics,constants",
            TextFr = "Combien vaut Pi (π) arrondi à deux décimales ?",
            TextNl = "Hoeveel is Pi (π) afgerond op twee decimalen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "3,14",
                AnswerNl = "3,14"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "Literature",
            Tags = "authors,classics",
            TextFr = "Qui a écrit '1984' ?",
            TextNl = "Wie schreef '1984'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "George Orwell",
                AnswerNl = "George Orwell"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "Geography",
            Tags = "countries,population",
            TextFr = "Quel est le pays le plus peuplé du monde ?",
            TextNl = "Wat is het meest bevolkte land ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Inde",
                AnswerNl = "India"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "History",
            Tags = "world-history,dates",
            TextFr = "En quelle année Christophe Colomb a-t-il découvert l'Amérique ?",
            TextNl = "In welk jaar ontdekte Christoffel Columbus Amerika?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1492",
                AnswerNl = "1492"
            }
        });

        // ==================== PHASE 4 REGULAR QUESTIONS (80 questions) ====================
        // Timer phase: Mostly easy, some medium, rare hard + trick questions with answer in question

        // EASY - Phase 4 (50 questions including trick questions)

        // Trick questions where answer is in the question (10)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Music",
            Tags = "trick,obvious",
            TextFr = "Qui a chanté la chanson 'Bohemian Rhapsody' de Queen ?",
            TextNl = "Wie zong het nummer 'Bohemian Rhapsody' van Queen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Queen",
                AnswerNl = "Queen"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Technology",
            Tags = "trick,obvious",
            TextFr = "Quelle entreprise a créé l'iPhone ?",
            TextNl = "Welk bedrijf maakte de iPhone?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Apple",
                AnswerNl = "Apple"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Science",
            Tags = "trick,obvious",
            TextFr = "Quel scientifique a développé la théorie de la relativité ?",
            TextNl = "Welke wetenschapper ontwikkelde de relativiteitstheorie?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Einstein",
                AnswerNl = "Einstein"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Sports",
            Tags = "trick,obvious",
            TextFr = "Quel sport joue les pongistes ?",
            TextNl = "Welke sport spelen tafeltennissers?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Tennis de table",
                AnswerNl = "Tafeltennis"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "History",
            Tags = "trick,obvious",
            TextFr = "Qui était le premier président américain ?",
            TextNl = "Wie was de eerste Amerikaanse president?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "George Washington",
                AnswerNl = "George Washington"
            }
        });

        // Regular easy questions (40)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "General",
            Tags = "days",
            TextFr = "Combien de jours y a-t-il dans une semaine ?",
            TextNl = "Hoeveel dagen heeft een week?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "7",
                AnswerNl = "7"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Food",
            Tags = "belgium,food",
            TextFr = "Quel pays est célèbre pour ses gaufres et son chocolat ?",
            TextNl = "Welk land is beroemd om zijn wafels en chocolade?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Belgique",
                AnswerNl = "België"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "pets",
            TextFr = "Quel animal dit 'miaou' ?",
            TextNl = "Welk dier zegt 'miauw'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Chat",
                AnswerNl = "Kat"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Colors",
            Tags = "basic",
            TextFr = "De quelle couleur est le ciel par beau temps ?",
            TextNl = "Welke kleur heeft de lucht bij mooi weer?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Bleu",
                AnswerNl = "Blauw"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Food",
            Tags = "fruits",
            TextFr = "Quel fruit est jaune et courbé ?",
            TextNl = "Welke vrucht is geel en gebogen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Banane",
                AnswerNl = "Banaan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Body",
            Tags = "senses",
            TextFr = "Avec quel organe voit-on ?",
            TextNl = "Met welk orgaan zien we?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Yeux",
                AnswerNl = "Ogen"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Numbers",
            Tags = "math",
            TextFr = "Combien font 100 - 25 ?",
            TextNl = "Hoeveel is 100 - 25?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "75",
                AnswerNl = "75"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Shapes",
            Tags = "geometry",
            TextFr = "Combien de côtés a un triangle ?",
            TextNl = "Hoeveel zijden heeft een driehoek?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "3",
                AnswerNl = "3"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Weather",
            Tags = "seasons",
            TextFr = "Quelle saison vient après l'hiver ?",
            TextNl = "Welk seizoen komt na de winter?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Printemps",
                AnswerNl = "Lente"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "farm",
            TextFr = "Quel animal donne du lait ?",
            TextNl = "Welk dier geeft melk?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Vache",
                AnswerNl = "Koe"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Transportation",
            Tags = "vehicles",
            TextFr = "Quel véhicule vole dans le ciel ?",
            TextNl = "Welk voertuig vliegt in de lucht?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Avion",
                AnswerNl = "Vliegtuig"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Nature",
            Tags = "plants",
            TextFr = "Quelle partie de la plante est sous terre ?",
            TextNl = "Welk deel van de plant zit onder de grond?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Racine",
                AnswerNl = "Wortel"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 3,
            IsActive = true,
            Category = "Music",
            Tags = "basic",
            TextFr = "Combien de notes y a-t-il dans une gamme ?",
            TextNl = "Hoeveel noten heeft een toonladder?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "7",
                AnswerNl = "7"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "continents",
            TextFr = "Sur quel continent se trouve la Belgique ?",
            TextNl = "Op welk continent ligt België?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Europe",
                AnswerNl = "Europa"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Astronomy",
            Tags = "space",
            TextFr = "Quel astre brille la nuit ?",
            TextNl = "Welk hemellichaam schijnt 's nachts?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Lune",
                AnswerNl = "Maan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Family",
            Tags = "relations",
            TextFr = "Comment appelle-t-on la mère de votre mère ?",
            TextNl = "Hoe noem je de moeder van je moeder?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Grand-mère",
                AnswerNl = "Grootmoeder"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "birds",
            TextFr = "Quel oiseau ne peut pas voler mais peut nager ?",
            TextNl = "Welke vogel kan niet vliegen maar wel zwemmen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pingouin",
                AnswerNl = "Pinguïn"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Water",
            Tags = "oceans",
            TextFr = "Comment appelle-t-on une grande étendue d'eau salée ?",
            TextNl = "Hoe noem je een grote watermassa met zout water?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Mer",
                AnswerNl = "Zee"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "House",
            Tags = "rooms",
            TextFr = "Dans quelle pièce de la maison dort-on ?",
            TextNl = "In welke kamer van het huis slaap je?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Chambre",
                AnswerNl = "Slaapkamer"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Time",
            Tags = "clock",
            TextFr = "Combien d'heures y a-t-il dans une journée ?",
            TextNl = "Hoeveel uur heeft een dag?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "24",
                AnswerNl = "24"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Cooking",
            Tags = "temperature",
            TextFr = "À quelle température l'eau gèle-t-elle ?",
            TextNl = "Bij welke temperatuur vriest water?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "0",
                AnswerNl = "0"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "jungle",
            TextFr = "Quel animal est le roi de la jungle ?",
            TextNl = "Welk dier is de koning van de jungle?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Lion",
                AnswerNl = "Leeuw"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Colors",
            Tags = "traffic",
            TextFr = "Quelle couleur signifie 'stop' au feu de circulation ?",
            TextNl = "Welke kleur betekent 'stop' bij het verkeerslicht?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Rouge",
                AnswerNl = "Rood"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "neighbors",
            TextFr = "Quel pays partage une frontière avec la Belgique et a Amsterdam comme capitale ?",
            TextNl = "Welk land grenst aan België en heeft Amsterdam als hoofdstad?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pays-Bas",
                AnswerNl = "Nederland"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Music",
            Tags = "christmas",
            TextFr = "Quelle fête célèbre-t-on le 25 décembre ?",
            TextNl = "Welk feest vieren we op 25 december?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Noël",
                AnswerNl = "Kerstmis"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Numbers",
            Tags = "math",
            TextFr = "Combien font 30 / 3 ?",
            TextNl = "Hoeveel is 30 / 3?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "10",
                AnswerNl = "10"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Body",
            Tags = "hands",
            TextFr = "Combien de doigts et de doigts de pied avons nous ?",
            TextNl = "Hoeveel vingers en tenen hebben we?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "20",
                AnswerNl = "20"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 1,
            IsActive = true,
            Category = "Alphabet",
            Tags = "letters",
            TextFr = "Quelle est la première lettre de l'alphabet ?",
            TextNl = "Wat is de eerste letter van het alfabet?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "A",
                AnswerNl = "A"
            }
        });

        // MEDIUM - Phase 4 (25 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de continents y a-t-il ?", TextNl = "Hoeveel continenten zijn er?", RegularDetails = new RegularQuestionDetails { AnswerFr = "7", AnswerNl = "7" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a peint la Joconde ?", TextNl = "Wie schilderde de Mona Lisa?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Léonard de Vinci", AnswerNl = "Leonardo da Vinci" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 12 x 8 ?", TextNl = "Hoeveel is 12 x 8?", RegularDetails = new RegularQuestionDetails { AnswerFr = "96", AnswerNl = "96" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale du Japon ?", TextNl = "Wat is de hoofdstad van Japan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Tokyo", AnswerNl = "Tokio" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de planètes y a-t-il dans notre système solaire ?", TextNl = "Hoeveel planeten zijn er in ons zonnestelsel?", RegularDetails = new RegularQuestionDetails { AnswerFr = "8", AnswerNl = "8" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus long fleuve d'Europe ?", TextNl = "Wat is de langste rivier van Europa?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Volga", AnswerNl = "Wolga" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 144 / 12 ?", TextNl = "Hoeveel is 144 / 12?", RegularDetails = new RegularQuestionDetails { AnswerFr = "12", AnswerNl = "12" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 15 x 15 ?", TextNl = "Hoeveel is 15 x 15?", RegularDetails = new RegularQuestionDetails { AnswerFr = "225", AnswerNl = "225" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus petit pays du monde ?", TextNl = "Wat is het kleinste land ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Vatican", AnswerNl = "Vaticaanstad" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 200 - 87 ?", TextNl = "Hoeveel is 200 - 87?", RegularDetails = new RegularQuestionDetails { AnswerFr = "113", AnswerNl = "113" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de la Russie ?", TextNl = "Wat is de hoofdstad van Rusland?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Moscou", AnswerNl = "Moskou" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 25 x 4 ?", TextNl = "Hoeveel is 25 x 4?", RegularDetails = new RegularQuestionDetails { AnswerFr = "100", AnswerNl = "100" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a découvert l'Amérique ?", TextNl = "Wie ontdekte Amerika?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Christophe Colomb", AnswerNl = "Christoffel Columbus" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le symbole chimique de l'argent ?", TextNl = "Wat is het chemisch symbool van zilver?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Ag", AnswerNl = "Ag" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 1000 / 25 ?", TextNl = "Hoeveel is 1000 / 25?", RegularDetails = new RegularQuestionDetails { AnswerFr = "40", AnswerNl = "40" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de la Chine ?", TextNl = "Wat is de hoofdstad van China?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Pékin", AnswerNl = "Peking" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de côtés a un hexagone ?", TextNl = "Hoeveel zijden heeft een zeshoek?", RegularDetails = new RegularQuestionDetails { AnswerFr = "6", AnswerNl = "6" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a écrit 'L'Odyssée' ?", TextNl = "Wie schreef 'De Odyssee'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Homère", AnswerNl = "Homerus" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 17 + 28 ?", TextNl = "Hoeveel is 17 + 28?", RegularDetails = new RegularQuestionDetails { AnswerFr = "45", AnswerNl = "45" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de l'Inde ?", TextNl = "Wat is de hoofdstad van India?", RegularDetails = new RegularQuestionDetails { AnswerFr = "New Delhi", AnswerNl = "New Delhi" } });
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Sports",
            Tags = "football,belgium",
            TextFr = "Quel est le surnom de l'équipe nationale belge de football ?",
            TextNl = "Wat is de bijnaam van het Belgisch voetbalelftal?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Diables Rouges",
                AnswerNl = "Rode Duivels"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "disney",
            TextFr = "Quel est le prénom de la princesse dans 'La Petite Sirène' ?",
            TextNl = "Wat is de voornaam van de prinses in 'De Kleine Zeemeermin'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Ariel",
                AnswerNl = "Ariel"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "inventions",
            TextFr = "Qui a inventé le téléphone ?",
            TextNl = "Wie heeft de telefoon uitgevonden?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Alexander Graham Bell",
                AnswerNl = "Alexander Graham Bell"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "cities,world",
            TextFr = "Quelle ville est surnommée 'La Grosse Pomme' ?",
            TextNl = "Welke stad wordt 'De Grote Appel' genoemd?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "New York",
                AnswerNl = "New York"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Science",
            Tags = "solar-system",
            TextFr = "Quelle planète est surnommée 'la planète rouge' ?",
            TextNl = "Welke planeet wordt 'de rode planeet' genoemd?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Mars",
                AnswerNl = "Mars"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Literature",
            Tags = "fairy-tales",
            TextFr = "Quel objet perd Cendrillon au bal ?",
            TextNl = "Welk voorwerp verliest Assepoester op het bal?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pantoufle de verre",
                AnswerNl = "Glazen muiltje"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "instruments",
            TextFr = "Combien de cordes a un violon ?",
            TextNl = "Hoeveel snaren heeft een viool?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "4",
                AnswerNl = "4"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Food",
            Tags = "belgium,beer",
            TextFr = "Quel pays est célèbre pour ses bières trappistes ?",
            TextNl = "Welk land is beroemd om zijn trappistenbieren?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Belgique",
                AnswerNl = "België"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Food",
            Tags = "cuisine,italy",
            TextFr = "Quelle ville italienne est célèbre pour sa tour penchée ?",
            TextNl = "Welke Italiaanse stad is beroemd om zijn scheve toren?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pise",
                AnswerNl = "Pisa"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Art",
            Tags = "painters,impressionism",
            TextFr = "Qui a peint 'Les Nymphéas' ?",
            TextNl = "Wie schilderde 'De Waterlelies'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Claude Monet",
                AnswerNl = "Claude Monet"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Technology",
            Tags = "social-media",
            TextFr = "Qui a fondé Facebook ?",
            TextNl = "Wie heeft Facebook opgericht?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Mark Zuckerberg",
                AnswerNl = "Mark Zuckerberg"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "superheroes",
            TextFr = "Quel est le vrai nom de Batman ?",
            TextNl = "Wat is de echte naam van Batman?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Bruce Wayne",
                AnswerNl = "Bruce Wayne"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "islands",
            TextFr = "Quelle est la plus grande île du monde ?",
            TextNl = "Wat is het grootste eiland ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Groenland",
                AnswerNl = "Groenland"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "bands",
            TextFr = "De quelle ville anglaise viennent les Beatles ?",
            TextNl = "Uit welke Engelse stad komen The Beatles?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Liverpool",
                AnswerNl = "Liverpool"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Sports",
            Tags = "olympics",
            TextFr = "Combien d'anneaux olympiques y a-t-il ?",
            TextNl = "Hoeveel olympische ringen zijn er?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "5",
                AnswerNl = "5"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "ancient",
            TextFr = "Quelle merveille du monde antique se trouve en Égypte ?",
            TextNl = "Welk wereldwonder van de oudheid staat in Egypte?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Pyramides de Gizeh",
                AnswerNl = "Piramides van Gizeh"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Nature",
            Tags = "flowers",
            TextFr = "Quelle fleur est le symbole des Pays-Bas ?",
            TextNl = "Welke bloem is het symbool van Nederland?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Tulipe",
                AnswerNl = "Tulp"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Science",
            Tags = "inventions",
            TextFr = "Qui a inventé l'ampoule électrique ?",
            TextNl = "Wie heeft de gloeilamp uitgevonden?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Thomas Edison",
                AnswerNl = "Thomas Edison"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "franchises",
            TextFr = "Comment s'appelle le château dans les films Harry Potter ?",
            TextNl = "Hoe heet het kasteel in de Harry Potter films?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Poudlard",
                AnswerNl = "Zweinstein"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Geography",
            Tags = "france,cities",
            TextFr = "Quelle ville française est connue pour sa cathédrale Notre-Dame ?",
            TextNl = "Welke Franse stad is gekend voor zijn Notre-Dame kathedraal?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Paris",
                AnswerNl = "Parijs"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Food",
            Tags = "drinks",
            TextFr = "Dans quel pays a été inventée la pizza ?",
            TextNl = "In welk land werd pizza uitgevonden?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Italie",
                AnswerNl = "Italië"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "Animals",
            Tags = "endangered",
            TextFr = "Quel animal noir et blanc mange du bambou ?",
            TextNl = "Welk zwart-wit dier eet bamboe?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Panda",
                AnswerNl = "Panda"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "ww2,dates",
            TextFr = "En quelle année s'est terminée la Seconde Guerre mondiale ?",
            TextNl = "In welk jaar eindigde de Tweede Wereldoorlog?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1945",
                AnswerNl = "1945"
            }
        });

        // HARD - Phase 4 (5 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a écrit 'À la recherche du temps perdu' ?", TextNl = "Wie schreef 'Op zoek naar de verloren tijd'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Marcel Proust", AnswerNl = "Marcel Proust" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la distance Terre-Lune moyenne (en km) ?", TextNl = "Wat is de gemiddelde afstand Aarde-Maan (in km)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "384400", AnswerNl = "384400" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale de la Mongolie ?", TextNl = "Wat is de hoofdstad van Mongolië?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Oulan-Bator", AnswerNl = "Ulaanbaatar" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a formulé les lois du mouvement ?", TextNl = "Wie formuleerde de bewegingswetten?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Isaac Newton", AnswerNl = "Isaac Newton" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale du Kazakhstan ?", TextNl = "Wat is de hoofdstad van Kazachstan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Astana", AnswerNl = "Astana" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a peint 'La Nuit étoilée' ?", TextNl = "Wie schilderde 'De Sterrennacht'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Vincent van Gogh", AnswerNl = "Vincent van Gogh" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la température du zéro absolu (en °C) ?", TextNl = "Wat is de temperatuur van het absolute nulpunt (in °C)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "-273.15", AnswerNl = "-273.15" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Combien font 2 puissance 10 ?", TextNl = "Hoeveel is 2 tot de macht 10?", RegularDetails = new RegularQuestionDetails { AnswerFr = "1024", AnswerNl = "1024" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale de l'Azerbaïdjan ?", TextNl = "Wat is de hoofdstad van Azerbeidzjan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bakou", AnswerNl = "Bakoe" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a découvert la pénicilline ?", TextNl = "Wie ontdekte penicilline?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Alexander Fleming", AnswerNl = "Alexander Fleming" } });
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 3,
            IsActive = true,
            Category = "Science",
            Tags = "chemistry,elements",
            TextFr = "Quel est le numéro atomique de l'oxygène ?",
            TextNl = "Wat is het atoomnummer van zuurstof?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "8",
                AnswerNl = "8"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 3,
            IsActive = true,
            Category = "Geography",
            Tags = "capitals,world",
            TextFr = "Quelle est la capitale de l'Australie ?",
            TextNl = "Wat is de hoofdstad van Australië?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Canberra",
                AnswerNl = "Canberra"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 3,
            IsActive = true,
            Category = "Literature",
            Tags = "nobel,authors",
            TextFr = "Qui a écrit 'Le Vieil Homme et la Mer' ?",
            TextNl = "Wie schreef 'De Oude Man en de Zee'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Ernest Hemingway",
                AnswerNl = "Ernest Hemingway"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular4,
            Difficulty = 3,
            IsActive = true,
            Category = "Music",
            Tags = "classical,composers",
            TextFr = "Combien de symphonies Beethoven a-t-il composées ?",
            TextNl = "Hoeveel symfonieën componeerde Beethoven?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "9",
                AnswerNl = "9"
            }
        });

        // ==================== PRIORITY QUESTIONS ====================

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Science",
            Tags = "chemistry,gold",
            TextFr = "De combien de carats est constitué l'or pur ?",
            TextNl = "Van hoeveel karaat bestaat puur goud?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "24 carats",
                AnswerNl = "24 karaat"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "population,countries",
            TextFr = "Quel pays est le moins peuplé du monde ?",
            TextNl = "Welk land is het minst bevolkt ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Vatican",
                AnswerNl = "Vaticaan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Movies",
            Tags = "harry-potter,characters",
            TextFr = "De quelle couleur sont les cheveux de Ron Weasley ?",
            TextNl = "Welke kleur heeft Ron Weasley zijn haar?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Roux",
                AnswerNl = "Rood"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "General Knowledge",
            Tags = "trivia,measurements",
            TextFr = "Quelle est la taille standard d'un cure-dent à 1cm près ?",
            TextNl = "Wat is de standaardlengte van een tandenstoker binnen 1 cm?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "5 à 6 cm",
                AnswerNl = "5 tot 6 cm"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Science",
            Tags = "chemistry,water",
            TextFr = "Quelle est la formule chimique de l'eau ?",
            TextNl = "Wat is de chemische formule van water?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "H2O",
                AnswerNl = "H2O"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "space,nasa,cities",
            TextFr = "Dans quelle ville s'entraînent les astronautes de la NASA dans le Centre spatial Lyndon B. Johnson ?",
            TextNl = "In welke stad trainen de astronauten van NASA in het Lyndon B. Johnson Space Center?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Houston",
                AnswerNl = "Houston"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "usa,states",
            TextFr = "Combien y a-t-il d'Etats américains ?",
            TextNl = "Hoeveel staten zijn er in de VS?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "50",
                AnswerNl = "50"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Science",
            Tags = "biology,genetics",
            TextFr = "Chromosome : qui du père ou de la mère détermine le sexe de l'enfant ?",
            TextNl = "Chromosoom: wie van de vader of moeder bepaalt het geslacht van het kind?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Le père",
                AnswerNl = "De vader"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Mythology",
            Tags = "greek-mythology,gods",
            TextFr = "Comment s'appelle le Dieu du vin dans la mythologie grecque ?",
            TextNl = "Hoe heet de God van de wijn in de Griekse mythologie?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Dionysos (Bacchus pour les romains)",
                AnswerNl = "Dionysos (Bacchus voor de Romeinen)"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Literature",
            Tags = "shakespeare,classics",
            TextFr = "Qui a écrit Roméo et Juliette ?",
            TextNl = "Wie heeft Romeo en Julia geschreven?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Shakespeare",
                AnswerNl = "Shakespeare"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Art",
            Tags = "painting,leonardo-da-vinci",
            TextFr = "Qui a peint la Joconde ?",
            TextNl = "Wie heeft de Mona Lisa geschilderd?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Léonard de Vinci",
                AnswerNl = "Leonardo da Vinci"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "rivers,world-records",
            TextFr = "Quel est le plus long fleuve du monde ?",
            TextNl = "Wat is de langste rivier ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Le Nil",
                AnswerNl = "De Nijl"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Literature",
            Tags = "victor-hugo,classics",
            TextFr = "Qui a écrit Les Misérables ?",
            TextNl = "Wie heeft Les Misérables geschreven?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Victor Hugo",
                AnswerNl = "Victor Hugo"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Sports",
            Tags = "football,world-cup",
            TextFr = "Quel pays a remporté la Coupe du Monde de football en 2018 ?",
            TextNl = "Welk land heeft het Wereldkampioenschap voetbal 2018 gewonnen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "La France",
                AnswerNl = "Frankrijk"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Fun Facts",
            Tags = "human-body,trivia",
            TextFr = "Un être humain produit assez de salive dans sa vie pour remplir combien de piscines ?",
            TextNl = "Een mens produceert genoeg speeksel in zijn leven om hoeveel zwembaden te vullen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "9",
                AnswerNl = "9"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Science",
            Tags = "sleep,human-body",
            TextFr = "En combien de minutes en moyenne s'endort un humain ?",
            TextNl = "In hoeveel minuten valt een mens gemiddeld in slaap?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "15 minutes",
                AnswerNl = "15 minuten"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "belgium,distances,personal",
            TextFr = "Combien y a-t-il de KM entre Brugge et Lauw ?",
            TextNl = "Hoeveel kilometer is het van Brugge naar Lauw?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "182 km",
                AnswerNl = "182 km"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "Personal",
            Tags = "personal,relationships",
            TextFr = "Depuis combien d'années Jonathan et Lies sont-ils en couple ?",
            TextNl = "Hoeveel jaar zijn Jonathan en Lies al samen?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "8 ans",
                AnswerNl = "8 jaar"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            IsPriority = true,
            Category = "History",
            Tags = "usa,presidents",
            TextFr = "Qui a été le premier président des États-Unis ?",
            TextNl = "Wie was de eerste president van de Verenigde Staten?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "George Washington",
                AnswerNl = "George Washington"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Television",
            Tags = "belgium,tv-shows",
            TextFr = "Qui est l'animateur de l'émission 'De Slimste Mens' ?",
            TextNl = "Wie is de presentator van het programma 'De Slimste Mens'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Erik Van Looy",
                AnswerNl = "Erik Van Looy"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "History",
            Tags = "belgium,jacques-brel,music",
            TextFr = "En quelle année est né Jacques Brel ?",
            TextNl = "In welk jaar werd Jacques Brel geboren?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1929 à Schaerbeek",
                AnswerNl = "1929 in Schaerbeek"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Music",
            Tags = "belgium,pop-rock,clouseau",
            TextFr = "Quel est le nom du groupe belge de pop-rock connu pour le tube suivant :",
            TextNl = "Wat is de naam van de Belgische pop-rock band die bekend is voor dit nummer:",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Clouseau",
                AnswerNl = "Clouseau"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "History",
            Tags = "ancient-civilizations,writing",
            TextFr = "Lequel des empires suivants n'avait pas de langue écrite : l'inca, l'aztèque, l'égyptien, le romain ?",
            TextNl = "Welk van de volgende rijken had geen geschreven taal: het Inca-rijk, het Azteekse rijk, het Egyptische rijk, het Romeinse rijk?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Inca",
                AnswerNl = "Inca"
            }
        });

        // ==================== PRIORITY LIST QUESTIONS ====================

        var listPriorityQuestion1 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "countries,south-america",
            TextFr = "Liste tous les pays d'Amérique du Sud",
            TextNl = "Noem alle landen van Zuid-Amerika"
        };
        listPriorityQuestion1.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Argentine", AnswerNl = "Argentinië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Bolivie", AnswerNl = "Bolivië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Brésil", AnswerNl = "Brazilië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Chili", AnswerNl = "Chili" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Colombie", AnswerNl = "Colombia" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Équateur", AnswerNl = "Ecuador" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Guyana", AnswerNl = "Guyana" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Paraguay", AnswerNl = "Paraguay" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Pérou", AnswerNl = "Peru" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Suriname", AnswerNl = "Suriname" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Uruguay", AnswerNl = "Uruguay" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion1.Id, AnswerFr = "Venezuela", AnswerNl = "Venezuela" }
        };
        questions.Add(listPriorityQuestion1);

        var listPriorityQuestion2 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Wildlife",
            Tags = "animals,felines",
            TextFr = "Citer les félins dont le nom contient la lettre A",
            TextNl = "Noem de katachtigen waarvan de naam de letter A bevat"
        };
        listPriorityQuestion2.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Jaguar", AnswerNl = "Jaguar" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Chat", AnswerNl = "Kat" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Léopard", AnswerNl = "Luipaard" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Panthère", AnswerNl = "Panter" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Puma", AnswerNl = "Puma" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Guépard", AnswerNl = "Cheetah" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Serval", AnswerNl = "Serval" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Margay", AnswerNl = "Margay" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Caracal", AnswerNl = "Caracal" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion2.Id, AnswerFr = "Jaguarondi", AnswerNl = "Jaguarondi" }
        };
        questions.Add(listPriorityQuestion2);

        var listPriorityQuestion3 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Geography",
            Tags = "europe,european-union,countries",
            TextFr = "Liste les pays membres de l'Union Européenne (UE) en 2025",
            TextNl = "Noem de landen die lid zijn van de Europese Unie (EU) in 2025"
        };
        listPriorityQuestion3.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Allemagne", AnswerNl = "Duitsland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Autriche", AnswerNl = "Oostenrijk" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Belgique", AnswerNl = "België" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Bulgarie", AnswerNl = "Bulgarije" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Chypre", AnswerNl = "Cyprus" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Croatie", AnswerNl = "Kroatië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Danemark", AnswerNl = "Denemarken" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Espagne", AnswerNl = "Spanje" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Estonie", AnswerNl = "Estland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Finlande", AnswerNl = "Finland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "France", AnswerNl = "Frankrijk" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Grèce", AnswerNl = "Griekenland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Hongrie", AnswerNl = "Hongarije" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Irlande", AnswerNl = "Ierland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Italie", AnswerNl = "Italië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Lettonie", AnswerNl = "Letland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Lituanie", AnswerNl = "Litouwen" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Luxembourg", AnswerNl = "Luxemburg" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Malte", AnswerNl = "Malta" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Pays-Bas", AnswerNl = "Nederland" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Pologne", AnswerNl = "Polen" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Portugal", AnswerNl = "Portugal" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "République tchèque", AnswerNl = "Tsjechië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Roumanie", AnswerNl = "Roemenië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Slovaquie", AnswerNl = "Slovenië" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Slovénie", AnswerNl = "Slowakije" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion3.Id, AnswerFr = "Suède", AnswerNl = "Zweden" }
        };
        questions.Add(listPriorityQuestion3);

        var listPriorityQuestion4 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Movies",
            Tags = "disney,lion-king,characters",
            TextFr = "Liste les noms des personnages qui parlent dans le dessin animé 'Le roi lion' (1994)",
            TextNl = "Noem de namen van de personages die praten in de animatiefilm 'De Leeuwenkoning' (1994)"
        };
        listPriorityQuestion4.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Simba", AnswerNl = "Simba" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Nala", AnswerNl = "Nala" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Mufasa", AnswerNl = "Mufasa" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Scar", AnswerNl = "Scar" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Timon", AnswerNl = "Timon" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Pumbaa", AnswerNl = "Pumbaa" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Rafiki", AnswerNl = "Rafiki" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Zazu", AnswerNl = "Zazu" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Sarabi", AnswerNl = "Sarabi" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Shenzi", AnswerNl = "Shenzi" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Banzai", AnswerNl = "Banzai" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion4.Id, AnswerFr = "Ed", AnswerNl = "Ed" }
        };
        questions.Add(listPriorityQuestion4);

        var listPriorityQuestion5 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            IsPriority = true,
            Category = "Cars",
            Tags = "automobiles,brands",
            TextFr = "Liste le plus de marques de voiture possible",
            TextNl = "Noem zoveel mogelijk automerken"
        };
        listPriorityQuestion5.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Toyota", AnswerNl = "Toyota" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Ford", AnswerNl = "Ford" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "BMW", AnswerNl = "BMW" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Mercedes", AnswerNl = "Mercedes" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Audi", AnswerNl = "Audi" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Honda", AnswerNl = "Honda" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Renault", AnswerNl = "Renault" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Ferrari", AnswerNl = "Ferrari" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Tesla", AnswerNl = "Tesla" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Volkswagen", AnswerNl = "Volkswagen" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Nissan", AnswerNl = "Nissan" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Peugeot", AnswerNl = "Peugeot" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Lamborghini", AnswerNl = "Lamborghini" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Hyundai", AnswerNl = "Hyundai" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Kia", AnswerNl = "Kia" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Subaru", AnswerNl = "Subaru" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Jaguar", AnswerNl = "Jaguar" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Volvo", AnswerNl = "Volvo" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Chevrolet", AnswerNl = "Chevrolet" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Mazda", AnswerNl = "Mazda" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Aston Martin", AnswerNl = "Aston Martin" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Alfa Romeo", AnswerNl = "Alfa Romeo" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Bugatti", AnswerNl = "Bugatti" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Citroën", AnswerNl = "Citroën" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Seat", AnswerNl = "Seat" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Skoda", AnswerNl = "Skoda" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "McLaren", AnswerNl = "McLaren" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Maserati", AnswerNl = "Maserati" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Rolls-Royce", AnswerNl = "Rolls-Royce" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Bentley", AnswerNl = "Bentley" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Dodge", AnswerNl = "Dodge" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Cadillac", AnswerNl = "Cadillac" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Jeep", AnswerNl = "Jeep" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Chrysler", AnswerNl = "Chrysler" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Buick", AnswerNl = "Buick" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Pontiac", AnswerNl = "Pontiac" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Lexus", AnswerNl = "Lexus" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Lotus", AnswerNl = "Lotus" },
            new() { Id = Guid.NewGuid(), QuestionId = listPriorityQuestion5.Id, AnswerFr = "Pagani", AnswerNl = "Pagani" }
        };
        questions.Add(listPriorityQuestion5);

        // ==================== PRIORITY REGULAR4 QUESTIONS ====================

        // Difficulty 1 (21 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Harry Potter", Tags = "movies,characters", TextFr = "Comment s'appelle les 2 amis d'Harry Potter ?", TextNl = "Hoe heten de twee vrienden van Harry Potter?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Ron et Hermione", AnswerNl = "Ron en Hermione" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Anatomy", Tags = "biology,human-body", TextFr = "Dans quelle partie du corps humain se situe le cerveau ?", TextNl = "In welk deel van het menselijke lichaam bevindt zich de hersenen?", RegularDetails = new RegularQuestionDetails { AnswerFr = "La tête", AnswerNl = "Het hoofd" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Language", Tags = "latin,vocabulary", TextFr = "Si \"recto\" signifie en latin \"à l'endroit\", quel mot latin est communément utilisé pour dire \"à l'envers\" ?", TextNl = "Als \"recto\" in het Latijn \"recht\" betekent, welk Latijns woord wordt dan meestal gebruikt om \"omgekeerd\" te zeggen?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Verso", AnswerNl = "Verso" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Animals", Tags = "marine-life,wildlife", TextFr = "Quel est le plus gros animal marin ?", TextNl = "Wat is het grootste zee-dier?", RegularDetails = new RegularQuestionDetails { AnswerFr = "La baleine bleue", AnswerNl = "De blauwe walvis" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Mathematics", Tags = "arithmetic,multiplication", TextFr = "Combien font 7x8 ?", TextNl = "Hoeveel is 7x8?", RegularDetails = new RegularQuestionDetails { AnswerFr = "56", AnswerNl = "56" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Literature", Tags = "biography,books", TextFr = "Qui a écrit \"le journal d'Anne Frank\" ?", TextNl = "Wie heeft \"Het dagboek van Anne Frank\" geschreven?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Anne Frank", AnswerNl = "Anne Frank" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Music", Tags = "instruments,strings", TextFr = "Quel est l'instrument de musique à cordes se joue avec un archet ?", TextNl = "Welk snaarinstrument wordt bespeeld met een strijkstok?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Le violon", AnswerNl = "De viool" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Geography", Tags = "mountains,africa", TextFr = "Quel est le plus haut sommet d'Afrique ?", TextNl = "Wat is de hoogste berg in Afrika?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Le Kilimandjaro", AnswerNl = "De Kilimanjaro" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Culture", Tags = "japan,nicknames", TextFr = "Quel pays est surnommé le « pays du Soleil-Levant » ?", TextNl = "Welk land wordt de \"Land van de Rijzende Zon\" genoemd?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Le Japon", AnswerNl = "Japan" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Biology", Tags = "respiration,gases", TextFr = "Quel gaz les humains respirent principalement ?", TextNl = "Welk gas ademen mensen voornamelijk in?", RegularDetails = new RegularQuestionDetails { AnswerFr = "L'oxygène", AnswerNl = "Zuurstof" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Music", Tags = "belgium,stromae,songs", TextFr = "Quel chanteur belge est connu pour la chanson Alors on danse ?", TextNl = "Welke Belgische zanger is bekend van het lied 'Alors on danse'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Stromae", AnswerNl = "Stromae" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Music", Tags = "instruments,piano", TextFr = "Quel instrument a 88 touches ?", TextNl = "Welk instrument heeft 88 toetsen?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Piano", AnswerNl = "Piano" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Movies", Tags = "star-wars,quotes", TextFr = "Quel film a pour célèbre réplique : Je suis ton père ?", TextNl = "Welke film heeft de beroemde uitspraak: 'Ik ben je vader'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Star Wars : L'Empire contre-attaque", AnswerNl = "Star Wars: The Empire Strikes Back" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Movies", Tags = "titanic,leonardo-dicaprio", TextFr = "Dans quel film Leonardo DiCaprio joue-t-il un personnage nommé Jack ?", TextNl = "In welke film speelt Leonardo DiCaprio een personage genaamd Jack?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Titanic", AnswerNl = "Titanic" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Animals", Tags = "personal,chickens", TextFr = "Est-ce que les poules de Jonathan et Lies pondent des oeufs ?", TextNl = "Leggen de kippen van Jonathan en Lies eieren?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Non", AnswerNl = "Nee" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Personal", Tags = "personal,names", TextFr = "Quel est le deuxième prénom de Céleste ?", TextNl = "Wat is de tweede naam van Céleste?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Aucun", AnswerNl = "Geen" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Animals", Tags = "personal,pets,cats", TextFr = "Comment s'appelle le chat de Jonathan et Lies ?", TextNl = "Hoe heet de kat van Jonathan en Lies?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Filou", AnswerNl = "Filou" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Personal", Tags = "personal,age", TextFr = "Quel âge a Jonathan aujourd'hui ?", TextNl = "Hoe oud is Jonathan vandaag?", RegularDetails = new RegularQuestionDetails { AnswerFr = "29 ans", AnswerNl = "29 jaar" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Music", Tags = "belgium,k3,songs", TextFr = "Quel est le nom du célèbre groupe de musique flamand qui écrit la chanson \"Oya lélé\" ?", TextNl = "Wat is de naam van de beroemde Vlaamse muziekgroep die het lied \"Oya lélé\" schrijft?", RegularDetails = new RegularQuestionDetails { AnswerFr = "K3", AnswerNl = "K3" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "True or False", Tags = "world-records,trivia", TextFr = "Vrai ou Faux : Un homme détient le record du monde pour avoir dormi debout pendant 48 heures.", TextNl = "Waar of niet waar: Een man heeft het wereldrecord voor het staan slapen gedurende 48 uur.", RegularDetails = new RegularQuestionDetails { AnswerFr = "VRAI", AnswerNl = "WAAR" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "True or False", Tags = "world-records,food", TextFr = "Vrai ou Faux : Le record du monde du plus grand nombre de donuts mangés en une minute est de 8 donuts.", TextNl = "Waar of niet waar: Het wereldrecord voor het grootste aantal donuts gegeten in één minuut is 8 donuts.", RegularDetails = new RegularQuestionDetails { AnswerFr = "Faux (12 donuts)", AnswerNl = "Onwaar (12 donuts)" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "True or False", Tags = "australia,animals", TextFr = "Vrai ou faux : l'Australie compte 10 fois plus de chameaux que de Koalas ?", TextNl = "Waar of niet waar: Australië heeft 10 keer zoveel kamelen als koala's?", RegularDetails = new RegularQuestionDetails { AnswerFr = "VRAI", AnswerNl = "WAAR" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Geography", Tags = "belgium,brussels,landmarks", TextFr = "Dans quelle ville peut-on admirer le \"Manneken-Pis\" ?", TextNl = "In welke stad kan men het 'Manneken-Pis' bewonderen?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bruxelles", AnswerNl = "Brussel" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Sports", Tags = "curling,ice-sports", TextFr = "Sur quelle surface se joue le curling ?", TextNl = "Op welk oppervlak wordt curling gespeeld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "La glace", AnswerNl = "Het ijs" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Technology", Tags = "keyboards,computers", TextFr = "Combien y a-t-il de lettres sur un clavier français/néerlandais ?", TextNl = "Hoeveel letters staan er op een Frans/Nederlands toetsenbord?", RegularDetails = new RegularQuestionDetails { AnswerFr = "26", AnswerNl = "26" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, IsPriority = true, Category = "Geography", Tags = "belgium,flags", TextFr = "Combien de couleurs sont présentes sur le drapeau de la Belgique ?", TextNl = "Hoeveel kleuren zijn er op de Belgische vlag?", RegularDetails = new RegularQuestionDetails { AnswerFr = "3", AnswerNl = "3" } });

        // Difficulty 2 (3 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, IsPriority = true, Category = "History", Tags = "products,kleenex,trivia", TextFr = "Quelle était la fonction principale du mouchoir lors de sa commercialisation par Kleenex en 1924 ?", TextNl = "Wat was de belangrijkste functie van de zakdoek bij de commercialisatie van Kleenex in 1924?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Du papier doux pour le démaquillage", AnswerNl = "Zacht papier voor make-upverwijdering" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, IsPriority = true, Category = "History", Tags = "fun-facts,world-war", TextFr = "La première guerre mondiale a-t-elle été remportée par le Real Madrid après prolongation ?", TextNl = "Heeft Real Madrid de Eerste Wereldoorlog gewonnen na verlengingen?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Non", AnswerNl = "Nee" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, IsPriority = true, Category = "Geography", Tags = "size,continents,greenland", TextFr = "Le Groenland est-il plus grand ou plus petit que l'Afrique ?", TextNl = "Is Groenland groter of kleiner dan Afrika?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Plus petit (d'environ 15 fois)", AnswerNl = "Kleiner (ongeveer 15 keer)" } });

        // Add all questions to the context
        context.Questions.AddRange(questions);
        context.SaveChanges();
    }
}
