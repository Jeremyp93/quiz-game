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

        // Add all questions to the context
        context.Questions.AddRange(questions);
        context.SaveChanges();
    }
}
