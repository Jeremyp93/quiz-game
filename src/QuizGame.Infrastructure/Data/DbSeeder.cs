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
        var themeAnimals = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "animals",
            NameFr = "Animaux",
            NameNl = "Dieren",
            IsActive = true,
            SortOrder = 1
        };

        var themeSports = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "sports",
            NameFr = "Sports",
            NameNl = "Sport",
            IsActive = true,
            SortOrder = 2
        };

        var themeTechnology = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "technology",
            NameFr = "Technologie",
            NameNl = "Technologie",
            IsActive = true,
            SortOrder = 3
        };

        var themeMovies = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "movies",
            NameFr = "Cinéma",
            NameNl = "Film",
            IsActive = true,
            SortOrder = 4
        };

        var themeArt = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "art",
            NameFr = "Art",
            NameNl = "Kunst",
            IsActive = true,
            SortOrder = 5
        };

        var themeMusic = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "music",
            NameFr = "Musique",
            NameNl = "Muziek",
            IsActive = true,
            SortOrder = 6
        };

        var themeHistory = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "history",
            NameFr = "Histoire",
            NameNl = "Geschiedenis",
            IsActive = true,
            SortOrder = 7
        };

        var themeScience = new Theme
        {
            Id = Guid.NewGuid(),
            Code = "science",
            NameFr = "Sciences",
            NameNl = "Wetenschappen",
            IsActive = true,
            SortOrder = 8
        };

        context.Themes.AddRange(new[] {
            themeAnimals, themeSports, themeTechnology, themeMovies,
            themeArt, themeMusic, themeHistory, themeScience
        });
        context.SaveChanges();

        var questions = new List<Question>();

        // ==================== REGULAR QUESTIONS ====================

        // Easy Regular Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "capitals,europe",
            TextFr = "Quelle est la capitale de la France ?",
            TextNl = "Wat is de hoofdstad van Frankrijk?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Paris",
                AnswerNl = "Parijs"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Science",
            Tags = "chemistry,elements",
            TextFr = "Quel est le symbole chimique de l'eau ?",
            TextNl = "Wat is het chemisch symbool voor water?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "H2O",
                AnswerNl = "H2O"
            }
        });

        // Medium Regular Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "world-war,dates",
            TextFr = "En quelle année a commencé la Seconde Guerre mondiale ?",
            TextNl = "In welk jaar begon de Tweede Wereldoorlog?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "1939",
                AnswerNl = "1939"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Literature",
            Tags = "authors,classic",
            TextFr = "Qui a écrit 'Les Misérables' ?",
            TextNl = "Wie schreef 'Les Misérables'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Victor Hugo",
                AnswerNl = "Victor Hugo"
            }
        });

        // Hard Regular Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "Science",
            Tags = "physics,constants",
            TextFr = "Quelle est la vitesse de la lumière dans le vide (en km/s) ?",
            TextNl = "Wat is de lichtsnelheid in vacuüm (in km/s)?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "299792",
                AnswerNl = "299792"
            }
        });

        // ==================== MCQ QUESTIONS ====================

        // Easy MCQ Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "mammals,classification",
            TextFr = "Quel animal est le plus grand mammifère terrestre ?",
            TextNl = "Welk dier is het grootste landzoogdier?",
            ThemeId = themeAnimals.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Éléphant d'Afrique",
                ChoiceANl = "Afrikaanse olifant",
                ChoiceBFr = "Girafe",
                ChoiceBNl = "Giraffe",
                ChoiceCFr = "Rhinocéros",
                ChoiceCNl = "Neushoorn",
                CorrectChoice = McqChoice.A
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            Category = "Sports",
            Tags = "olympics,track",
            TextFr = "Combien de mètres fait un sprint olympique court ?",
            TextNl = "Hoeveel meter is een korte olympische sprint?",
            ThemeId = themeSports.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "50 mètres",
                ChoiceANl = "50 meter",
                ChoiceBFr = "100 mètres",
                ChoiceBNl = "100 meter",
                ChoiceCFr = "200 mètres",
                ChoiceCNl = "200 meter",
                CorrectChoice = McqChoice.B
            }
        });

        // Medium MCQ Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            Category = "Technology",
            Tags = "computers,programming",
            TextFr = "Quel langage de programmation est principalement utilisé pour le développement Android ?",
            TextNl = "Welke programmeertaal wordt vooral gebruikt voor Android-ontwikkeling?",
            ThemeId = themeTechnology.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Swift",
                ChoiceANl = "Swift",
                ChoiceBFr = "Python",
                ChoiceBNl = "Python",
                ChoiceCFr = "Kotlin",
                ChoiceCNl = "Kotlin",
                CorrectChoice = McqChoice.C
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            Category = "Movies",
            Tags = "oscars,directors",
            TextFr = "Qui a réalisé 'Pulp Fiction' ?",
            TextNl = "Wie regisseerde 'Pulp Fiction'?",
            ThemeId = themeMovies.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Steven Spielberg",
                ChoiceANl = "Steven Spielberg",
                ChoiceBFr = "Quentin Tarantino",
                ChoiceBNl = "Quentin Tarantino",
                ChoiceCFr = "Martin Scorsese",
                ChoiceCNl = "Martin Scorsese",
                CorrectChoice = McqChoice.B
            }
        });

        // Hard MCQ Questions
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            Category = "Art",
            Tags = "painting,renaissance",
            TextFr = "Dans quel musée se trouve 'La Naissance de Vénus' de Botticelli ?",
            TextNl = "In welk museum bevindt zich Botticelli's 'De geboorte van Venus'?",
            ThemeId = themeArt.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Le Louvre",
                ChoiceANl = "Het Louvre",
                ChoiceBFr = "La Galerie des Offices",
                ChoiceBNl = "De Uffizi Galerij",
                ChoiceCFr = "Le Musée du Prado",
                ChoiceCNl = "Het Prado Museum",
                CorrectChoice = McqChoice.B
            }
        });

        // Additional MCQ questions to complete theme sets for Phase 3 testing

        // ANIMALS theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Combien de pattes a une araignée ?",
            TextNl = "Hoeveel poten heeft een spin?",
            ThemeId = themeAnimals.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "6",
                ChoiceANl = "6",
                ChoiceBFr = "8",
                ChoiceBNl = "8",
                ChoiceCFr = "10",
                ChoiceCNl = "10",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
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
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Quel est l'animal le plus venimeux au monde ?",
            TextNl = "Wat is het giftigste dier ter wereld?",
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
        });

        // SPORTS theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Combien de joueurs y a-t-il dans une équipe de football ?",
            TextNl = "Hoeveel spelers zitten er in een voetbalteam?",
            ThemeId = themeSports.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "9",
                ChoiceANl = "9",
                ChoiceBFr = "11",
                ChoiceBNl = "11",
                ChoiceCFr = "13",
                ChoiceCNl = "13",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            TextFr = "Dans quel pays se sont déroulés les Jeux Olympiques de 2016 ?",
            TextNl = "In welk land vonden de Olympische Spelen van 2016 plaats?",
            ThemeId = themeSports.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Chine",
                ChoiceANl = "China",
                ChoiceBFr = "Brésil",
                ChoiceBNl = "Brazilië",
                ChoiceCFr = "Royaume-Uni",
                ChoiceCNl = "Verenigd Koninkrijk",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Qui détient le record du monde de médailles olympiques ?",
            TextNl = "Wie heeft het wereldrecord voor olympische medailles?",
            ThemeId = themeSports.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Usain Bolt",
                ChoiceANl = "Usain Bolt",
                ChoiceBFr = "Michael Phelps",
                ChoiceBNl = "Michael Phelps",
                ChoiceCFr = "Simone Biles",
                ChoiceCNl = "Simone Biles",
                CorrectChoice = McqChoice.B
            }
        });

        // TECHNOLOGY theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Quelle entreprise a créé l'iPhone ?",
            TextNl = "Welk bedrijf heeft de iPhone gemaakt?",
            ThemeId = themeTechnology.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Samsung",
                ChoiceANl = "Samsung",
                ChoiceBFr = "Apple",
                ChoiceBNl = "Apple",
                ChoiceCFr = "Google",
                ChoiceCNl = "Google",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Que signifie WWW dans une adresse internet ?",
            TextNl = "Wat betekent WWW in een internetadres?",
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
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Qui est considéré comme le père de l'informatique moderne ?",
            TextNl = "Wie wordt beschouwd als de vader van de moderne informatica?",
            ThemeId = themeTechnology.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Steve Jobs",
                ChoiceANl = "Steve Jobs",
                ChoiceBFr = "Alan Turing",
                ChoiceBNl = "Alan Turing",
                ChoiceCFr = "Bill Gates",
                ChoiceCNl = "Bill Gates",
                CorrectChoice = McqChoice.B
            }
        });

        // MOVIES theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Quel film a remporté l'Oscar du meilleur film en 1998 ?",
            TextNl = "Welke film won de Oscar voor beste film in 1998?",
            ThemeId = themeMovies.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Saving Private Ryan",
                ChoiceANl = "Saving Private Ryan",
                ChoiceBFr = "Titanic",
                ChoiceBNl = "Titanic",
                ChoiceCFr = "The Truman Show",
                ChoiceCNl = "The Truman Show",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Dans quel film trouve-t-on la réplique 'Je suis ton père' ?",
            TextNl = "In welke film komt de zin 'Ik ben je vader' voor?",
            ThemeId = themeMovies.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Star Trek",
                ChoiceANl = "Star Trek",
                ChoiceBFr = "Star Wars",
                ChoiceBNl = "Star Wars",
                ChoiceCFr = "Retour vers le futur",
                ChoiceCNl = "Terug naar de toekomst",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Quel acteur a joué dans le plus de films de l'histoire ?",
            TextNl = "Welke acteur speelde in de meeste films in de geschiedenis?",
            ThemeId = themeMovies.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Samuel L. Jackson",
                ChoiceANl = "Samuel L. Jackson",
                ChoiceBFr = "Christopher Lee",
                ChoiceBNl = "Christopher Lee",
                ChoiceCFr = "Robert De Niro",
                ChoiceCNl = "Robert De Niro",
                CorrectChoice = McqChoice.B
            }
        });

        // ART theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Qui a peint la Joconde ?",
            TextNl = "Wie schilderde de Mona Lisa?",
            ThemeId = themeArt.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Michelangelo",
                ChoiceANl = "Michelangelo",
                ChoiceBFr = "Leonardo da Vinci",
                ChoiceBNl = "Leonardo da Vinci",
                ChoiceCFr = "Raphael",
                ChoiceCNl = "Raphael",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Quelle couleur obtient-on en mélangeant rouge et jaune ?",
            TextNl = "Welke kleur krijg je door rood en geel te mengen?",
            ThemeId = themeArt.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Vert",
                ChoiceANl = "Groen",
                ChoiceBFr = "Orange",
                ChoiceBNl = "Oranje",
                ChoiceCFr = "Violet",
                ChoiceCNl = "Violet",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            TextFr = "Quel artiste a coupé son oreille ?",
            TextNl = "Welke kunstenaar sneed zijn oor af?",
            ThemeId = themeArt.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Pablo Picasso",
                ChoiceANl = "Pablo Picasso",
                ChoiceBFr = "Vincent van Gogh",
                ChoiceBNl = "Vincent van Gogh",
                ChoiceCFr = "Claude Monet",
                ChoiceCNl = "Claude Monet",
                CorrectChoice = McqChoice.B
            }
        });

        // HISTORY theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "En quelle année l'homme a-t-il marché sur la lune pour la première fois ?",
            TextNl = "In welk jaar liep de mens voor het eerst op de maan?",
            ThemeId = themeHistory.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "1965",
                ChoiceANl = "1965",
                ChoiceBFr = "1969",
                ChoiceBNl = "1969",
                ChoiceCFr = "1972",
                ChoiceCNl = "1972",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Qui était le premier président des États-Unis ?",
            TextNl = "Wie was de eerste president van de Verenigde Staten?",
            ThemeId = themeHistory.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Thomas Jefferson",
                ChoiceANl = "Thomas Jefferson",
                ChoiceBFr = "George Washington",
                ChoiceBNl = "George Washington",
                ChoiceCFr = "Abraham Lincoln",
                ChoiceCNl = "Abraham Lincoln",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            TextFr = "Quelle civilisation a construit le Machu Picchu ?",
            TextNl = "Welke beschaving bouwde Machu Picchu?",
            ThemeId = themeHistory.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Les Aztèques",
                ChoiceANl = "De Azteken",
                ChoiceBFr = "Les Incas",
                ChoiceBNl = "De Inca's",
                ChoiceCFr = "Les Mayas",
                ChoiceCNl = "De Maya's",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "En quelle année est tombé le mur de Berlin ?",
            TextNl = "In welk jaar viel de Berlijnse Muur?",
            ThemeId = themeHistory.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "1987",
                ChoiceANl = "1987",
                ChoiceBFr = "1989",
                ChoiceBNl = "1989",
                ChoiceCFr = "1991",
                ChoiceCNl = "1991",
                CorrectChoice = McqChoice.B
            }
        });

        // SCIENCE theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Quelle planète est la plus proche du Soleil ?",
            TextNl = "Welke planeet staat het dichtst bij de zon?",
            ThemeId = themeScience.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Vénus",
                ChoiceANl = "Venus",
                ChoiceBFr = "Mercure",
                ChoiceBNl = "Mercurius",
                ChoiceCFr = "Mars",
                ChoiceCNl = "Mars",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Combien d'os y a-t-il dans le corps humain adulte ?",
            TextNl = "Hoeveel botten heeft een volwassen menselijk lichaam?",
            ThemeId = themeScience.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "186",
                ChoiceANl = "186",
                ChoiceBFr = "206",
                ChoiceBNl = "206",
                ChoiceCFr = "226",
                ChoiceCNl = "226",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            TextFr = "Quel est l'élément chimique le plus abondant dans l'univers ?",
            TextNl = "Wat is het meest voorkomende chemische element in het universum?",
            ThemeId = themeScience.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Oxygène",
                ChoiceANl = "Zuurstof",
                ChoiceBFr = "Hydrogène",
                ChoiceBNl = "Waterstof",
                ChoiceCFr = "Hélium",
                ChoiceCNl = "Helium",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Quelle particule subatomique a été découverte au CERN en 2012 ?",
            TextNl = "Welk subatomair deeltje werd in 2012 ontdekt bij CERN?",
            ThemeId = themeScience.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Le quark",
                ChoiceANl = "De quark",
                ChoiceBFr = "Le boson de Higgs",
                ChoiceBNl = "Het Higgs-deeltje",
                ChoiceCFr = "Le neutrino",
                ChoiceCNl = "De neutrino",
                CorrectChoice = McqChoice.B
            }
        });

        // MUSIC theme (complete set: 2×Diff1, 1×Diff2, 1×Diff3)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Combien de cordes a une guitare classique ?",
            TextNl = "Hoeveel snaren heeft een klassieke gitaar?",
            ThemeId = themeMusic.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "4",
                ChoiceANl = "4",
                ChoiceBFr = "6",
                ChoiceBNl = "6",
                ChoiceCFr = "8",
                ChoiceCNl = "8",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 1,
            IsActive = true,
            TextFr = "Quel instrument Freddie Mercury jouait-il ?",
            TextNl = "Welk instrument bespeelde Freddie Mercury?",
            ThemeId = themeMusic.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Guitare",
                ChoiceANl = "Gitaar",
                ChoiceBFr = "Piano",
                ChoiceBNl = "Piano",
                ChoiceCFr = "Batterie",
                ChoiceCNl = "Drums",
                CorrectChoice = McqChoice.B
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 2,
            IsActive = true,
            TextFr = "Quel est le vrai nom d'Elton John ?",
            TextNl = "Wat is de echte naam van Elton John?",
            ThemeId = themeMusic.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Reginald Dwight",
                ChoiceANl = "Reginald Dwight",
                ChoiceBFr = "David Jones",
                ChoiceBNl = "David Jones",
                ChoiceCFr = "Robert Zimmerman",
                ChoiceCNl = "Robert Zimmerman",
                CorrectChoice = McqChoice.A
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Mcq,
            Difficulty = 3,
            IsActive = true,
            TextFr = "Quel compositeur est devenu sourd à la fin de sa vie ?",
            TextNl = "Welke componist werd doof op het einde van zijn leven?",
            ThemeId = themeMusic.Id,
            McqDetails = new McqQuestionDetails
            {
                ChoiceAFr = "Mozart",
                ChoiceANl = "Mozart",
                ChoiceBFr = "Beethoven",
                ChoiceBNl = "Beethoven",
                ChoiceCFr = "Bach",
                ChoiceCNl = "Bach",
                CorrectChoice = McqChoice.B
            }
        });

        // ==================== LIST QUESTIONS ====================

        // Easy List Questions
        var listQuestion1 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "continents,world",
            TextFr = "Nommez les 7 continents",
            TextNl = "Noem de 7 continenten"
        };
        listQuestion1.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Afrique", AnswerNl = "Afrika" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Antarctique", AnswerNl = "Antarctica" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Asie", AnswerNl = "Azië" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Europe", AnswerNl = "Europa" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Amérique du Nord", AnswerNl = "Noord-Amerika" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Océanie", AnswerNl = "Oceanië" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion1.Id, AnswerFr = "Amérique du Sud", AnswerNl = "Zuid-Amerika" }
        };
        questions.Add(listQuestion1);

        var listQuestion2 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 1,
            IsActive = true,
            Category = "Science",
            Tags = "planets,solar-system",
            TextFr = "Nommez les 4 planètes rocheuses du système solaire",
            TextNl = "Noem de 4 rotsplaneten in ons zonnestelsel"
        };
        listQuestion2.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion2.Id, AnswerFr = "Mercure", AnswerNl = "Mercurius" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion2.Id, AnswerFr = "Vénus", AnswerNl = "Venus" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion2.Id, AnswerFr = "Terre", AnswerNl = "Aarde" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion2.Id, AnswerFr = "Mars", AnswerNl = "Mars" }
        };
        questions.Add(listQuestion2);

        // Medium List Questions
        var listQuestion3 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            Category = "History",
            Tags = "belgium,regions",
            TextFr = "Nommez les 3 régions de Belgique",
            TextNl = "Noem de 3 gewesten van België"
        };
        listQuestion3.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion3.Id, AnswerFr = "Région flamande", AnswerNl = "Vlaams Gewest", AltSpellings = "Flandre,Vlaanderen" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion3.Id, AnswerFr = "Région wallonne", AnswerNl = "Waals Gewest", AltSpellings = "Wallonie,Wallonië" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion3.Id, AnswerFr = "Région de Bruxelles-Capitale", AnswerNl = "Brussels Hoofdstedelijk Gewest", AltSpellings = "Bruxelles,Brussel" }
        };
        questions.Add(listQuestion3);

        var listQuestion4 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 2,
            IsActive = true,
            Category = "Music",
            Tags = "beatles,members",
            TextFr = "Nommez les 4 membres des Beatles",
            TextNl = "Noem de 4 leden van The Beatles"
        };
        listQuestion4.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion4.Id, AnswerFr = "John Lennon", AnswerNl = "John Lennon" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion4.Id, AnswerFr = "Paul McCartney", AnswerNl = "Paul McCartney" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion4.Id, AnswerFr = "George Harrison", AnswerNl = "George Harrison" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion4.Id, AnswerFr = "Ringo Starr", AnswerNl = "Ringo Starr" }
        };
        questions.Add(listQuestion4);

        // Hard List Questions
        var listQuestion5 = new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.List,
            Difficulty = 3,
            IsActive = true,
            Category = "Science",
            Tags = "chemistry,noble-gases",
            TextFr = "Nommez les 6 gaz nobles",
            TextNl = "Noem de 6 edelgassen"
        };
        listQuestion5.ListAnswers = new List<ListQuestionAnswer>
        {
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Hélium", AnswerNl = "Helium" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Néon", AnswerNl = "Neon" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Argon", AnswerNl = "Argon" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Krypton", AnswerNl = "Krypton" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Xénon", AnswerNl = "Xenon" },
            new() { Id = Guid.NewGuid(), QuestionId = listQuestion5.Id, AnswerFr = "Radon", AnswerNl = "Radon" }
        };
        questions.Add(listQuestion5);

        // ==================== REGULAR4 QUESTIONS (PHASE 4 - CHRONO) ====================
        // 80 questions with mixed difficulty for Phase 4

        // Difficulty 1 (30 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien y a-t-il de jours dans une semaine ?", TextNl = "Hoeveel dagen zitten er in een week?", RegularDetails = new RegularQuestionDetails { AnswerFr = "7", AnswerNl = "7" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle est la couleur du ciel par beau temps ?", TextNl = "Wat is de kleur van de lucht bij mooi weer?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bleu", AnswerNl = "Blauw" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 5 + 5 ?", TextNl = "Hoeveel is 5 + 5?", RegularDetails = new RegularQuestionDetails { AnswerFr = "10", AnswerNl = "10" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel animal miaule ?", TextNl = "Welk dier miauwt?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Chat", AnswerNl = "Kat" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien de roues a une voiture ?", TextNl = "Hoeveel wielen heeft een auto?", RegularDetails = new RegularQuestionDetails { AnswerFr = "4", AnswerNl = "4" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle est la capitale de la Belgique ?", TextNl = "Wat is de hoofdstad van België?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bruxelles", AnswerNl = "Brussel" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien y a-t-il de saisons dans une année ?", TextNl = "Hoeveel seizoenen zijn er in een jaar?", RegularDetails = new RegularQuestionDetails { AnswerFr = "4", AnswerNl = "4" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel fruit est rouge et rond ?", TextNl = "Welke vrucht is rood en rond?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Pomme", AnswerNl = "Appel" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 10 - 3 ?", TextNl = "Hoeveel is 10 - 3?", RegularDetails = new RegularQuestionDetails { AnswerFr = "7", AnswerNl = "7" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel animal aboie ?", TextNl = "Welk dier blaft?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Chien", AnswerNl = "Hond" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien y a-t-il de mois dans une année ?", TextNl = "Hoeveel maanden zitten er in een jaar?", RegularDetails = new RegularQuestionDetails { AnswerFr = "12", AnswerNl = "12" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "De quelle couleur est le soleil ?", TextNl = "Welke kleur heeft de zon?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Jaune", AnswerNl = "Geel" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 2 x 5 ?", TextNl = "Hoeveel is 2 x 5?", RegularDetails = new RegularQuestionDetails { AnswerFr = "10", AnswerNl = "10" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel est l'opposé de chaud ?", TextNl = "Wat is het tegenovergestelde van warm?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Froid", AnswerNl = "Koud" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien y a-t-il d'heures dans une journée ?", TextNl = "Hoeveel uren zitten er in een dag?", RegularDetails = new RegularQuestionDetails { AnswerFr = "24", AnswerNl = "24" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle planète est la plus proche du Soleil ?", TextNl = "Welke planeet staat het dichtst bij de zon?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Mercure", AnswerNl = "Mercurius" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 15 - 10 ?", TextNl = "Hoeveel is 15 - 10?", RegularDetails = new RegularQuestionDetails { AnswerFr = "5", AnswerNl = "5" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle est la capitale de l'Italie ?", TextNl = "Wat is de hoofdstad van Italië?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Rome", AnswerNl = "Rome" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien de doigts avons-nous sur une main ?", TextNl = "Hoeveel vingers hebben we aan één hand?", RegularDetails = new RegularQuestionDetails { AnswerFr = "5", AnswerNl = "5" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel est l'opposé de grand ?", TextNl = "Wat is het tegenovergestelde van groot?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Petit", AnswerNl = "Klein" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 3 x 3 ?", TextNl = "Hoeveel is 3 x 3?", RegularDetails = new RegularQuestionDetails { AnswerFr = "9", AnswerNl = "9" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle est la capitale de l'Espagne ?", TextNl = "Wat is de hoofdstad van Spanje?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Madrid", AnswerNl = "Madrid" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 20 - 5 ?", TextNl = "Hoeveel is 20 - 5?", RegularDetails = new RegularQuestionDetails { AnswerFr = "15", AnswerNl = "15" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel animal hennit ?", TextNl = "Welk dier hinnikt?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Cheval", AnswerNl = "Paard" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "De quelle couleur est l'herbe ?", TextNl = "Welke kleur heeft gras?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Vert", AnswerNl = "Groen" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 4 x 2 ?", TextNl = "Hoeveel is 4 x 2?", RegularDetails = new RegularQuestionDetails { AnswerFr = "8", AnswerNl = "8" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quelle est la capitale de l'Allemagne ?", TextNl = "Wat is de hoofdstad van Duitsland?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Berlin", AnswerNl = "Berlijn" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 100 - 50 ?", TextNl = "Hoeveel is 100 - 50?", RegularDetails = new RegularQuestionDetails { AnswerFr = "50", AnswerNl = "50" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Quel est l'opposé de haut ?", TextNl = "Wat is het tegenovergestelde van hoog?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bas", AnswerNl = "Laag" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 1, IsActive = true, TextFr = "Combien font 6 + 4 ?", TextNl = "Hoeveel is 6 + 4?", RegularDetails = new RegularQuestionDetails { AnswerFr = "10", AnswerNl = "10" } });

        // Difficulty 2 (30 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus grand océan du monde ?", TextNl = "Wat is de grootste oceaan ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Pacifique", AnswerNl = "Grote Oceaan" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de continents y a-t-il ?", TextNl = "Hoeveel continenten zijn er?", RegularDetails = new RegularQuestionDetails { AnswerFr = "7", AnswerNl = "7" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a peint la Joconde ?", TextNl = "Wie schilderde de Mona Lisa?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Léonard de Vinci", AnswerNl = "Leonardo da Vinci" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la plus haute montagne du monde ?", TextNl = "Wat is de hoogste berg ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Everest", AnswerNl = "Mount Everest" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 12 x 8 ?", TextNl = "Hoeveel is 12 x 8?", RegularDetails = new RegularQuestionDetails { AnswerFr = "96", AnswerNl = "96" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale du Japon ?", TextNl = "Wat is de hoofdstad van Japan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Tokyo", AnswerNl = "Tokio" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de planètes y a-t-il dans notre système solaire ?", TextNl = "Hoeveel planeten zijn er in ons zonnestelsel?", RegularDetails = new RegularQuestionDetails { AnswerFr = "8", AnswerNl = "8" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a écrit 'Roméo et Juliette' ?", TextNl = "Wie schreef 'Romeo en Julia'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Shakespeare", AnswerNl = "Shakespeare" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus long fleuve d'Europe ?", TextNl = "Wat is de langste rivier van Europa?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Volga", AnswerNl = "Wolga" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 144 / 12 ?", TextNl = "Hoeveel is 144 / 12?", RegularDetails = new RegularQuestionDetails { AnswerFr = "12", AnswerNl = "12" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de l'Australie ?", TextNl = "Wat is de hoofdstad van Australië?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Canberra", AnswerNl = "Canberra" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "En quelle année l'homme a-t-il marché sur la Lune ?", TextNl = "In welk jaar liep de mens op de maan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "1969", AnswerNl = "1969" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le symbole chimique de l'or ?", TextNl = "Wat is het chemisch symbool van goud?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Au", AnswerNl = "Au" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 15 x 15 ?", TextNl = "Hoeveel is 15 x 15?", RegularDetails = new RegularQuestionDetails { AnswerFr = "225", AnswerNl = "225" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale du Canada ?", TextNl = "Wat is de hoofdstad van Canada?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Ottawa", AnswerNl = "Ottawa" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a inventé l'ampoule électrique ?", TextNl = "Wie vond de gloeilamp uit?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Edison", AnswerNl = "Edison" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus petit pays du monde ?", TextNl = "Wat is het kleinste land ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Vatican", AnswerNl = "Vaticaanstad" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 200 - 87 ?", TextNl = "Hoeveel is 200 - 87?", RegularDetails = new RegularQuestionDetails { AnswerFr = "113", AnswerNl = "113" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de la Russie ?", TextNl = "Wat is de hoofdstad van Rusland?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Moscou", AnswerNl = "Moskou" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus grand désert du monde ?", TextNl = "Wat is de grootste woestijn ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Sahara", AnswerNl = "Sahara" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 25 x 4 ?", TextNl = "Hoeveel is 25 x 4?", RegularDetails = new RegularQuestionDetails { AnswerFr = "100", AnswerNl = "100" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a découvert l'Amérique ?", TextNl = "Wie ontdekte Amerika?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Christophe Colomb", AnswerNl = "Christoffel Columbus" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le symbole chimique de l'argent ?", TextNl = "Wat is het chemisch symbool van zilver?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Ag", AnswerNl = "Ag" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 1000 / 25 ?", TextNl = "Hoeveel is 1000 / 25?", RegularDetails = new RegularQuestionDetails { AnswerFr = "40", AnswerNl = "40" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de la Chine ?", TextNl = "Wat is de hoofdstad van China?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Pékin", AnswerNl = "Peking" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien de côtés a un hexagone ?", TextNl = "Hoeveel zijden heeft een zeshoek?", RegularDetails = new RegularQuestionDetails { AnswerFr = "6", AnswerNl = "6" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Qui a écrit 'L'Odyssée' ?", TextNl = "Wie schreef 'De Odyssee'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Homère", AnswerNl = "Homerus" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quel est le plus grand mammifère du monde ?", TextNl = "Wat is het grootste zoogdier ter wereld?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Baleine bleue", AnswerNl = "Blauwe vinvis" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Combien font 17 + 28 ?", TextNl = "Hoeveel is 17 + 28?", RegularDetails = new RegularQuestionDetails { AnswerFr = "45", AnswerNl = "45" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 2, IsActive = true, TextFr = "Quelle est la capitale de l'Inde ?", TextNl = "Wat is de hoofdstad van India?", RegularDetails = new RegularQuestionDetails { AnswerFr = "New Delhi", AnswerNl = "New Delhi" } });

        // Difficulty 3 (20 questions)
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la constante de Planck (en J·s) ?", TextNl = "Wat is de constante van Planck (in J·s)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "6.626×10⁻³⁴", AnswerNl = "6.626×10⁻³⁴" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a écrit 'À la recherche du temps perdu' ?", TextNl = "Wie schreef 'Op zoek naar de verloren tijd'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Marcel Proust", AnswerNl = "Marcel Proust" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la distance Terre-Lune moyenne (en km) ?", TextNl = "Wat is de gemiddelde afstand Aarde-Maan (in km)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "384400", AnswerNl = "384400" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quel est le nombre d'Avogadro ?", TextNl = "Wat is het getal van Avogadro?", RegularDetails = new RegularQuestionDetails { AnswerFr = "6.022×10²³", AnswerNl = "6.022×10²³" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Combien font 789 x 456 ?", TextNl = "Hoeveel is 789 x 456?", RegularDetails = new RegularQuestionDetails { AnswerFr = "359784", AnswerNl = "359784" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale de la Mongolie ?", TextNl = "Wat is de hoofdstad van Mongolië?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Oulan-Bator", AnswerNl = "Ulaanbaatar" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a formulé les lois du mouvement ?", TextNl = "Wie formuleerde de bewegingswetten?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Isaac Newton", AnswerNl = "Isaac Newton" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la masse molaire du glucose (en g/mol) ?", TextNl = "Wat is de molaire massa van glucose (in g/mol)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "180", AnswerNl = "180" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Combien font la racine carrée de 2704 ?", TextNl = "Hoeveel is de vierkantswortel van 2704?", RegularDetails = new RegularQuestionDetails { AnswerFr = "52", AnswerNl = "52" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale du Kazakhstan ?", TextNl = "Wat is de hoofdstad van Kazachstan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Astana", AnswerNl = "Astana" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a peint 'La Nuit étoilée' ?", TextNl = "Wie schilderde 'De Sterrennacht'?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Vincent van Gogh", AnswerNl = "Vincent van Gogh" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la température du zéro absolu (en °C) ?", TextNl = "Wat is de temperatuur van het absolute nulpunt (in °C)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "-273.15", AnswerNl = "-273.15" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Combien font 2 puissance 10 ?", TextNl = "Hoeveel is 2 tot de macht 10?", RegularDetails = new RegularQuestionDetails { AnswerFr = "1024", AnswerNl = "1024" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale de l'Azerbaïdjan ?", TextNl = "Wat is de hoofdstad van Azerbeidzjan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Bakou", AnswerNl = "Bakoe" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a découvert la pénicilline ?", TextNl = "Wie ontdekte penicilline?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Alexander Fleming", AnswerNl = "Alexander Fleming" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la formule de l'acide sulfurique ?", TextNl = "Wat is de formule van zwavelzuur?", RegularDetails = new RegularQuestionDetails { AnswerFr = "H2SO4", AnswerNl = "H2SO4" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Combien font 17² ?", TextNl = "Hoeveel is 17²?", RegularDetails = new RegularQuestionDetails { AnswerFr = "289", AnswerNl = "289" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la capitale du Bhoutan ?", TextNl = "Wat is de hoofdstad van Bhutan?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Thimphou", AnswerNl = "Thimphu" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Qui a développé la théorie de la relativité générale ?", TextNl = "Wie ontwikkelde de algemene relativiteitstheorie?", RegularDetails = new RegularQuestionDetails { AnswerFr = "Albert Einstein", AnswerNl = "Albert Einstein" } });
        questions.Add(new Question { Id = Guid.NewGuid(), Type = QuestionType.Regular4, Difficulty = 3, IsActive = true, TextFr = "Quelle est la vitesse du son dans l'air (en m/s) ?", TextNl = "Wat is de geluidssnelheid in lucht (in m/s)?", RegularDetails = new RegularQuestionDetails { AnswerFr = "343", AnswerNl = "343" } });

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
            TextFr = "Quelle est la capitale de la Belgique ?",
            TextNl = "Wat is de hoofdstad van België?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Bruxelles",
                AnswerNl = "Brussel"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
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
            TextFr = "Combien de minutes y a-t-il dans une heure ?",
            TextNl = "Hoeveel minuten heeft een uur?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "60",
                AnswerNl = "60"
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
            TextFr = "Combien de joueurs y a-t-il sur le terrain dans une équipe de football ?",
            TextNl = "Hoeveel spelers staan er op het veld in een voetbalteam?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "11",
                AnswerNl = "11"
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
            TextFr = "Comment s'appelle le château dans le dessin animé 'La Belle et la Bête' ?",
            TextNl = "Hoe heet de prins in het verhaal 'Belle en het Beest'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Le château de la Bête",
                AnswerNl = "Het Beest"
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
            TextFr = "Combien de fois Rafael Nadal a-t-il remporté Roland-Garros jusqu'en 2023 ?",
            TextNl = "Hoeveel keer won Rafael Nadal Roland-Garros tot 2023?",
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Literature",
            Tags = "trick,obvious",
            TextFr = "Qui a écrit le roman 'Les Misérables' écrit par Victor Hugo ?",
            TextNl = "Wie schreef de roman 'Les Misérables' geschreven door Victor Hugo?",
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
            Category = "Movies",
            Tags = "trick,obvious",
            TextFr = "Quel acteur a joué dans le film avec Tom Hanks dans le rôle principal ?",
            TextNl = "Welke acteur speelde in de film met Tom Hanks in de hoofdrol?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Tom Hanks",
                AnswerNl = "Tom Hanks"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Art",
            Tags = "trick,obvious",
            TextFr = "Qui a peint le tableau 'La Joconde' peint par Léonard de Vinci ?",
            TextNl = "Wie schilderde het schilderij 'De Mona Lisa' geschilderd door Leonardo da Vinci?",
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
            Category = "Technology",
            Tags = "trick,obvious",
            TextFr = "Quelle entreprise a créé l'iPhone fabriqué par Apple ?",
            TextNl = "Welk bedrijf maakte de iPhone gemaakt door Apple?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Apple",
                AnswerNl = "Apple"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Science",
            Tags = "trick,obvious",
            TextFr = "Quel scientifique a développé la théorie de la relativité d'Einstein ?",
            TextNl = "Welke wetenschapper ontwikkelde de relativiteitstheorie van Einstein?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Einstein",
                AnswerNl = "Einstein"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Sports",
            Tags = "trick,obvious",
            TextFr = "Quel sport se joue au tennis ?",
            TextNl = "Welke sport wordt gespeeld bij tennis?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Tennis",
                AnswerNl = "Tennis"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Geography",
            Tags = "trick,obvious",
            TextFr = "Dans quel pays se trouve la capitale de la France, Paris ?",
            TextNl = "In welk land ligt de hoofdstad van Frankrijk, Parijs?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "France",
                AnswerNl = "Frankrijk"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "History",
            Tags = "trick,obvious",
            TextFr = "Qui était le premier président américain George Washington ?",
            TextNl = "Wie was de eerste Amerikaanse president George Washington?",
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
            Difficulty = 1,
            IsActive = true,
            Category = "Literature",
            Tags = "trick,obvious",
            TextFr = "Qui a créé le personnage de Tintin créé par Hergé ?",
            TextNl = "Wie bedacht het personage Kuifje bedacht door Hergé?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Hergé",
                AnswerNl = "Hergé"
            }
        });

        // Regular easy questions (40)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Numbers",
            Tags = "math",
            TextFr = "Combien font 5 + 5 ?",
            TextNl = "Hoeveel is 5 + 5?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "10",
                AnswerNl = "10"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Food",
            Tags = "bread",
            TextFr = "Avec quelle céréale fait-on du pain ?",
            TextNl = "Met welk graan maakt men brood?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Blé",
                AnswerNl = "Tarwe"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Sports",
            Tags = "ball",
            TextFr = "Dans quel sport utilise-t-on une raquette ?",
            TextNl = "Bij welke sport gebruik je een racket?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Tennis",
                AnswerNl = "Tennis"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Technology",
            Tags = "devices",
            TextFr = "Avec quel appareil téléphone-t-on en déplacement ?",
            TextNl = "Met welk toestel bel je onderweg?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Téléphone portable",
                AnswerNl = "Gsm"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Clothing",
            Tags = "accessories",
            TextFr = "Que porte-t-on sur la tête quand il pleut ?",
            TextNl = "Wat draag je op je hoofd als het regent?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Chapeau",
                AnswerNl = "Hoed"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Animals",
            Tags = "sounds",
            TextFr = "Quel animal fait 'cocorico' ?",
            TextNl = "Welk dier maakt 'kukeleku'?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Coq",
                AnswerNl = "Haan"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Belgium",
            Tags = "languages,belgium",
            TextFr = "Combien de langues officielles la Belgique a-t-elle ?",
            TextNl = "Hoeveel officiële talen heeft België?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "3",
                AnswerNl = "3"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Food",
            Tags = "drinks",
            TextFr = "Quelle boisson provient de la vache ?",
            TextNl = "Welke drank komt van de koe?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Lait",
                AnswerNl = "Melk"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Nature",
            Tags = "trees",
            TextFr = "De quelle couleur sont généralement les feuilles en été ?",
            TextNl = "Welke kleur hebben bladeren meestal in de zomer?",
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
            Category = "Animals",
            Tags = "characteristics",
            TextFr = "Quel animal a une trompe ?",
            TextNl = "Welk dier heeft een slurf?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Éléphant",
                AnswerNl = "Olifant"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Money",
            Tags = "currency",
            TextFr = "Quelle monnaie utilise-t-on en Belgique ?",
            TextNl = "Welke munt gebruiken we in België?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Euro",
                AnswerNl = "Euro"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Numbers",
            Tags = "math",
            TextFr = "Combien font 10 - 5 ?",
            TextNl = "Hoeveel is 10 - 5?",
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
            Category = "Body",
            Tags = "hands",
            TextFr = "Combien de doigts a une main ?",
            TextNl = "Hoeveel vingers heeft een hand?",
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

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 1,
            IsActive = true,
            Category = "Weather",
            Tags = "rain",
            TextFr = "Qu'est-ce qui tombe du ciel quand il pleut ?",
            TextNl = "Wat valt er uit de lucht als het regent?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Eau",
                AnswerNl = "Water"
            }
        });

        // MEDIUM - Phase 4 (25 questions)
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Animals",
            Tags = "fastest",
            TextFr = "Quel est l'animal terrestre le plus rapide ?",
            TextNl = "Wat is het snelste landdier?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Guépard",
                AnswerNl = "Jachtluipaard"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
            Difficulty = 2,
            IsActive = true,
            Category = "Comics",
            Tags = "belgium,characters",
            TextFr = "Comment s'appelle le village gaulois d'Astérix ?",
            TextNl = "Hoe heet het Gallische dorp van Asterix?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Village gaulois",
                AnswerNl = "Gallisch dorp"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
            Difficulty = 3,
            IsActive = true,
            Category = "Geography",
            Tags = "world,deserts",
            TextFr = "Quel est le plus grand désert du monde ?",
            TextNl = "Wat is de grootste woestijn ter wereld?",
            RegularDetails = new RegularQuestionDetails
            {
                AnswerFr = "Sahara",
                AnswerNl = "Sahara"
            }
        });

        questions.Add(new Question
        {
            Id = Guid.NewGuid(),
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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
            Type = QuestionType.Regular,
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

        // Add all questions to the context
        context.Questions.AddRange(questions);
        context.SaveChanges();
    }
}
