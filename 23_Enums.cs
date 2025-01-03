using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    enum LogLevel
    {
        Trace = 1,
        Debug = 2,
        Info = 4,
        Warning = 5,
        Error = 6,
        Fatal = 42,
        Unknown = 0

    }

    static class LogLine
    {
        public static LogLevel ParseLogLevel(string logLine)
        {
            switch (logLine[1..4])
            {
                case "TRC":
                    return LogLevel.Trace;
                case "DBG":
                    return LogLevel.Debug;
                case "INF":
                    return LogLevel.Info;
                case "WRN":
                    return LogLevel.Warning;
                case "ERR":
                    return LogLevel.Error;
                case "FTL":
                    return LogLevel.Fatal;
                default:
                    return LogLevel.Unknown;
            }
        }

        public static string OutputForShortLog(LogLevel logLevel, string message) =>  $"{(int)logLevel}:{message}";

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------

    public enum YachtCategory
    {
        Ones = 1,
        Twos = 2,
        Threes = 3,
        Fours = 4,
        Fives = 5,
        Sixes = 6,
        FullHouse = 7,
        FourOfAKind = 8,
        LittleStraight = 9,
        BigStraight = 10,
        Choice = 11,
        Yacht = 12,
    }

    public static class YachtGame
    {
        public static int Score(int[] dice, YachtCategory category)
        {
            int categoryValue = (int)category;
            int[] uniqueValues = dice.Distinct().ToArray();
            int[] countValues = uniqueValues.Select(v => dice.Count(x => x == v)).ToArray();

            if (category >= YachtCategory.Ones &&  category <= YachtCategory.Sixes)
            {
                return categoryValue * dice.Count(x => x == categoryValue);
            } else if (category == YachtCategory.FullHouse)
            {
                if (countValues.Length == 2 &&
                    ((countValues[0] == 3 && countValues[1] == 2) ||
                    (countValues[1] == 3 && countValues[0] == 2)))
                {
                    return countValues[0] * uniqueValues[0] + countValues[1] * uniqueValues[1];
                }
            } else if (category == YachtCategory.FourOfAKind)
            {
                if (countValues[0] >= 4) return 4 * uniqueValues[0];
                if (countValues.Length > 1 && countValues[1] >= 4) return 4 * uniqueValues[1];
            } else if (category == YachtCategory.LittleStraight)
            {
                if (dice.OrderBy(x => x).SequenceEqual(new int[] { 1, 2, 3, 4, 5 })) return 30;
            } else if (category == YachtCategory.BigStraight)
            {
                if (dice.OrderBy(x => x).SequenceEqual(new int[] { 2, 3, 4, 5, 6 })) return 30;
            } else if (category == YachtCategory.Yacht)
            {
                if (countValues[0] == 5) return 50;
            } else
            {
                return dice.Sum();
            }

            return 0;   
        }
    }

    //Comunity Solution
    //public enum YachtCategory { Ones, Twos, Threes, Fours, Fives, Sixes, FullHouse, FourOfAKind, LittleStraight, BigStraight, Choice, Yacht }
    //public static class YachtGame
    //{
    //    private static readonly List<Func<int[], int>> scoring = new List<Func<int[], int>> {
    //    (int[] dice) => dice.ScoreSingles(1),
    //    (int[] dice) => dice.ScoreSingles(2),
    //    (int[] dice) => dice.ScoreSingles(3),
    //    (int[] dice) => dice.ScoreSingles(4),
    //    (int[] dice) => dice.ScoreSingles(5),
    //    (int[] dice) => dice.ScoreSingles(6),
    //    (int[] dice) => dice.ScoreFullHouse(),
    //    (int[] dice) => dice.ScoreFourOfAKind(),
    //    (int[] dice) => dice.ScoreStraight(15),
    //    (int[] dice) => dice.ScoreStraight(20),
    //    (int[] dice) => dice.Sum(),
    //    (int[] dice) => dice.GroupBy(x => x).Count() == 1 ? 50 : 0
    //};
    //    public static int Score(int[] dice, YachtCategory category) => scoring[(int)category](dice);
    //    private static int ScoreSingles(this int[] dice, int single) => (dice.GroupBy(x => x).FirstOrDefault(x => x.Key == single)?.Count() ?? 0) * single;
    //    private static int ScoreFullHouse(this int[] dice) => dice.GroupBy(x => x).All(g => new[] { 2, 3 }.Contains(g.Count())) ? dice.Sum() : 0;
    //    private static int ScoreStraight(this int[] dice, int sum) => dice.GroupBy(x => x).Count() == 5 && dice.Sum() == sum ? 30 : 0;
    //    private static int ScoreFourOfAKind(this int[] dice) => (dice.GroupBy(x => x).FirstOrDefault(g => g.Count() >= 4)?.First() ?? 0) * 4;
    //}


    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    //NO FUNCIONAAAAAAAA
    //public enum Color { Red, Green, Ivory, Yellow, Blue }
    //public enum Nationality { Englishman, Spaniard, Ukrainian, Japanese, Norwegian }
    //public enum Pet { Dog, Snails, Fox, Horse, Zebra }
    //public enum Drink { Coffee, Tea, Milk, OrangeJuice, Water }
    //public enum Activity { Dancing, Reading, Football, Chess, Painting }

    //public static class ZebraPuzzle
    //{
    //    private class element
    //    {
    //        public Color? color;
    //        public Nationality? nationality;
    //        public Pet? pet;
    //        public Drink? drink;
    //        public Activity? activity;
    //    }

    //    // Funciones para establecer los elementos
    //    private static void SetColor(element e, Color? newColor) { e.color = newColor; }
    //    private static void SetNationality(element e, Nationality? newNationality) { e.nationality = newNationality; }
    //    private static void SetPet(element e, Pet? newPet) { e.pet = newPet; }
    //    private static void SetDrink(element e, Drink? newDrink) { e.drink = newDrink; }
    //    private static void SetActivity(element e, Activity? newActivity) { e.activity = newActivity; }

    //    //1. There are five houses.
    //    private static element?[] elements = new element[5];
    //    private static element?[] orderedElements = new element[5];


    //    // Función para asignar la restricción
    //    private static bool AssignProperty<T,U>(T? property1,U? property2, Func<element,T?> getProperty1, Func<element,U?> getProperty2, Action<element,T?> setProperty1, Action<element, U?> setProperty2)
    //        where T : struct
    //        where U : struct
    //    {
    //        var existingElement = elements.Where(e => e != null).FirstOrDefault(e => Nullable.Equals(getProperty1(e), property1));
    //        var existingElement2 = elements.Where(e => e != null).FirstOrDefault(e => Nullable.Equals(getProperty2(e), property2));
    //        if (existingElement != null)
    //        {
    //            setProperty2(existingElement, property2);
    //        } else if (existingElement2 != null)
    //        {
    //            setProperty1(existingElement2, property1);
    //        }else
    //        {
    //            int firstEmptyElement_index = elements.ToList().FindIndex(e => e == null); //.FirstOrDefault(e => e == null);
    //            if (firstEmptyElement_index != -1)
    //            {
    //                elements[firstEmptyElement_index] = new element();
    //                var firstEmptyElement = elements[firstEmptyElement_index];
    //                setProperty1(firstEmptyElement, property1);
    //                setProperty2(firstEmptyElement, property2);
    //            }
    //            else if (firstEmptyElement_index == -1)
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                throw new InvalidOperationException("No se puede cumplir la restricción");
    //            }
    //        }
    //        return true;
    //    }

    //    // Función para completar faltantes
    //    private static void completeMissing<T>(Func<element, T?> checkFunction, Action<element, T?> assignFunction) where T : struct
    //    {
    //        T?[] assignedVariables = elements.Select(checkFunction).ToArray();
    //        if (assignedVariables.Count(x => x == null) == 1)
    //        {
    //            T?[] allVariables = Enum.GetValues(typeof(T)).Cast<T?>().ToArray();
    //            T?[] resultVariables = allVariables.Except(assignedVariables).ToArray();
    //            if (resultVariables.Count() == 1)
    //            {
    //                var elementToUpdate = elements.FirstOrDefault(x => x != null && checkFunction(x) == null);
    //                if (elementToUpdate != null)
    //                {
    //                    assignFunction(elementToUpdate, resultVariables[0]);
    //                }
    //            }
    //        }
    //    }

    //    //private static void Reorder<T,U>(T? property1, U? property2,Func<element,T?>getProperty1, Func<element,U?>? getProperty2, Action<element[]> position)
    //    //{
    //    //    element?[] orderedElements = new element?[5];

    //    //    element firstElement = elements.Where(e => e != null).FirstOrDefault(e => Nullable.Equals(getProperty1(e), property1));
    //    //    element secondElement = elements.Where(e => e != null).FirstOrDefault(e => Nullable.Equals(getProperty2(e), property2));


    //    //}

    //    //DELETEEEE!!!
    //    public static void Run()
    //    {
    //        Solve();
    //    }

    //    private static element[] Solve()
    //    {
    //        bool[] completedConstrains = new bool[15];

    //        //for (int i = 0; i<5; i++) elements[i] = new element();
    //        completedConstrains[0] = true;  //Cambiarlo por código que chequee si están todos los elementos repartidos o falta alguno?

    //        while (completedConstrains.Any(x => x == false))
    //        {
    //            element? elementOfInterest;

    //            #region 2. The Englishman lives in the red house.
    //            completedConstrains[1] = AssignProperty(Nationality.Englishman, Color.Red, 
    //                e => e.nationality, e => e.color, 
    //                SetNationality, SetColor);
                


    //            //if (elements.Where(x => x.color == Color.Red) != null)
    //            //{
    //            //    elementOfInterest = elements.Where(x => x.color == Color.Red).ToArray()[0];
    //            //    elementOfInterest.nationality = Nationality.Englishman;
    //            //    completedConstrains[1] = true;
    //            //}
    //            //else if (elements.Where(x => x.nationality == Nationality.Englishman) != null)
    //            //{
    //            //    elementOfInterest = elements.Where(x => x.nationality == Nationality.Englishman).ToArray()[0];
    //            //    elementOfInterest.color = Color.Red;
    //            //    completedConstrains[1] = true;
    //            //}
    //            //else
    //            //{
    //            //    for (int i = 0; i<elements.Length; i++)
    //            //    {
    //            //        if (elements[i] == null)
    //            //        {
    //            //            element element = new element();
    //            //            element.nationality = Nationality.Englishman;
    //            //            element.color = Color.Red;
    //            //            break;
    //            //        }
    //            //    }
    //            //}
    //            #endregion

    //            #region 3. The Spaniard owns the dog.
    //            completedConstrains[2] = AssignProperty(Nationality.Spaniard, Pet.Dog,
    //                e => e.nationality, e => e.pet,
    //                SetNationality, SetPet);
    //            #endregion

    //            #region 4. The person in the green house drinks coffee.
    //            completedConstrains[3] = AssignProperty(Color.Green, Drink.Coffee,
    //                e => e.color, e => e.drink,
    //                SetColor, SetDrink);
    //            #endregion

    //            #region 5. The Ukrainian drinks tea.
    //            completedConstrains[4] = AssignProperty(Nationality.Ukrainian, Drink.Tea,
    //                e => e.nationality, e => e.drink,
    //                SetNationality, SetDrink);
    //            #endregion

    //            #region 6. The green house is immediately to the right of the ivory house.
    //            //AssignProperty(Nationality.Ukrainian, Drink.Tea,
    //            //    e => e.nationality, e => e.drink,
    //            //    SetNationality, SetDrink);


    //            int ivoryHouseIndex = orderedElements.Where(x => x!= null).ToList().FindIndex(x => x.color == Color.Ivory);
    //            if (ivoryHouseIndex != -1) orderedElements[ivoryHouseIndex + 1] = elements.FirstOrDefault(x => x.color == Color.Green);

    //            if (ivoryHouseIndex != -1 && orderedElements[ivoryHouseIndex + 1] != null) completedConstrains[5] = true;
    //            #endregion

    //            #region 7. The snail owner likes to go dancing.
    //            completedConstrains[6] = AssignProperty(Pet.Snails, Activity.Dancing,
    //                x => x.pet, x => x.activity,
    //                SetPet, SetActivity);

    //            #endregion

    //            #region 8. The person in the yellow house is a painter.
    //            completedConstrains[7] = AssignProperty(Color.Yellow, Activity.Painting,
    //                x => x.color, x => x.activity,
    //                SetColor, SetActivity);
    //            #endregion

    //            #region 9. The person in the middle house drinks milk.
    //            element? actualElement = elements.Where(x => x != null).FirstOrDefault(x => x.drink == Drink.Milk);
    //            if (actualElement != null) orderedElements[2] = actualElement;
    //            else if (orderedElements[2] != null) orderedElements[2].drink = Drink.Milk;
    //            if (orderedElements[2] != null && orderedElements[2].drink == Drink.Milk) completedConstrains[8] = true;
    //            #endregion

    //            #region 10. The Norwegian lives in the first house.
    //            actualElement = elements.Where(x => x != null).FirstOrDefault(x => x.nationality == Nationality.Norwegian);
    //            completedConstrains[9] = true;
    //            if (actualElement != null) orderedElements[0] = actualElement;
    //            else if (orderedElements[0] != null) orderedElements[0].nationality = Nationality.Norwegian;
    //            else completedConstrains[9] = false;
    //            #endregion

    //            #region 11. The person who enjoys reading lives in the house next to the person with the fox.
    //            int elementEnjoyReading = orderedElements.Where((x) => x != null).ToList().FindIndex(x => x.activity == Activity.Reading);
    //            int elementPetFox = orderedElements.Where(x => x != null).ToList().FindIndex(x => x.pet == Pet.Fox);

    //            if (elementEnjoyReading != -1)
    //            {
    //                element? previous = elementEnjoyReading > 0 ? orderedElements[elementEnjoyReading - 1] : null;
    //                element? next = elementEnjoyReading < orderedElements.Count() - 1 ? orderedElements[elementEnjoyReading + 1] : null;
    //                //Si alguno es nulo
    //                // Añadir a la posición anterior
    //                if (previous == null && next != null && next.pet != Pet.Fox) orderedElements[elementEnjoyReading - 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.pet == Pet.Fox);
    //                // Añadir a la posición siguiente
    //                if (previous != null && next == null && previous.pet != Pet.Fox) orderedElements[elementEnjoyReading + 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.pet == Pet.Fox);
    //                //Si ninguno es nulo
    //                if ((previous.pet != Pet.Fox && previous.pet != null) && next.pet == null) next.pet = Pet.Fox;
    //                if (previous.pet == null && (next.pet != Pet.Fox && next.pet != null)) previous.pet = Pet.Fox;

    //                if(previous.pet == Pet.Fox || next.pet == Pet.Fox) completedConstrains[10] = true;

    //            }
    //            else if (elementPetFox != -1)
    //            {
    //                element? previous = elementPetFox > 0 ? orderedElements[elementPetFox - 1] : null;
    //                element? next = elementPetFox < orderedElements.Count() - 1 ? orderedElements[elementPetFox + 1] : null;
    //                //Si alguno es nulo
    //                // Añadir a la posición anterior
    //                if (previous == null && next != null && next.activity != Activity.Reading) orderedElements[elementPetFox - 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.activity == Activity.Reading);
    //                // Añadir a la posición siguiente
    //                if (previous != null && next == null && previous.activity != Activity.Reading) orderedElements[elementPetFox + 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.activity == Activity.Reading);
    //                //Si ninguno es nulo
    //                if ((previous.activity != Activity.Reading && previous.activity != null) && next.activity == null) next.activity = Activity.Reading;
    //                if (previous.activity == null && (next.activity != Activity.Reading && next.activity != null)) previous.activity = Activity.Reading;

    //                if (previous.activity == Activity.Reading || next.activity == Activity.Reading) completedConstrains[10] = true;
    //            }
    //            #endregion

    //            #region 12. The painter's house is next to the house with the horse.
    //            int elementEnjoyPainting = orderedElements.Where((x) => x != null).ToList().FindIndex(x => x.activity == Activity.Painting);
    //            int elementPetHorse = orderedElements.Where(x => x != null).ToList().FindIndex(x => x.pet == Pet.Horse);

    //            if (elementEnjoyPainting != -1)
    //            {
    //                element? previous = elementEnjoyPainting > 0 ? orderedElements[elementEnjoyPainting - 1] : null;
    //                element? next = elementEnjoyPainting < orderedElements.Count() - 1 ? orderedElements[elementEnjoyPainting + 1] : null;
    //                //Si alguno es nulo
    //                // Añadir a la posición anterior
    //                if (previous == null && next != null && next.pet != Pet.Horse) orderedElements[elementEnjoyPainting - 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.pet == Pet.Horse);
    //                // Añadir a la posición siguiente
    //                if (previous != null && next == null && previous.pet != Pet.Horse) orderedElements[elementEnjoyPainting + 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.pet == Pet.Horse);
    //                //Si ninguno es nulo
    //                if ((previous.pet != Pet.Horse && previous.pet != null) && next.pet == null) next.pet = Pet.Horse;
    //                if (previous.pet == null && (next.pet != Pet.Horse && next.pet != null)) previous.pet = Pet.Horse;

    //                if (previous.pet == Pet.Horse || next.pet == Pet.Horse) completedConstrains[11] = true;

    //            }
    //            else if (elementPetHorse != -1)
    //            {
    //                element? previous = elementPetHorse > 0 ? orderedElements[elementPetHorse - 1] : null;
    //                element? next = elementPetHorse < orderedElements.Count() - 1 ? orderedElements[elementPetHorse + 1] : null;
    //                //Si alguno es nulo
    //                // Añadir a la posición anterior
    //                if (previous == null && next != null && next.activity != Activity.Painting) orderedElements[elementPetHorse - 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.activity == Activity.Painting);
    //                // Añadir a la posición siguiente
    //                if (previous != null && next == null && previous.activity != Activity.Painting) orderedElements[elementPetHorse + 1] = elements.Where((x) => x != null).FirstOrDefault(x => x.activity == Activity.Painting);
    //                //Si ninguno es nulo
    //                if ((previous.activity != Activity.Painting && previous.activity != null) && next.activity == null) next.activity = Activity.Painting;
    //                if (previous.activity == null && (next.activity != Activity.Painting && next.activity != null)) previous.activity = Activity.Painting;

    //                if (previous.activity == Activity.Painting || next.activity == Activity.Painting) completedConstrains[11] = true;
    //            }

    //            #endregion

    //            #region 13. The person who plays football drinks orange juice.
    //            completedConstrains[12] = AssignProperty(Activity.Football, Drink.OrangeJuice,
    //                x => x.activity, x => x.drink,
    //                SetActivity, SetDrink);

    //            #endregion

    //            #region 14. The Japanese person plays chess.
    //            completedConstrains[13] = AssignProperty(Nationality.Japanese, Activity.Chess,
    //                x => x.nationality, x => x.activity,
    //                SetNationality, SetActivity);
    //            #endregion

    //            #region 15. The Norwegian lives next to the blue house.
    //            completedConstrains[14] = AssignProperty(Nationality.Norwegian, Color.Blue,
    //                x => x.nationality, x => x.color,
    //                SetNationality, SetColor);
    //            #endregion


    //            //Añadimos los elementos restantes y posiciones restantes

    //            completeMissing(x => x.color, (x, value) => x.color = value);
    //            completeMissing(x => x.nationality, (x, value) => x.nationality = value);
    //            completeMissing(x => x.drink, (x, value) => x.drink = value);
    //            completeMissing(x => x.activity, (x, value) => x.activity = value);
    //            completeMissing(x => x.pet, (x, value) => x.pet = value);

    //            if (elements.Where(x => x != null).Count() == 5 && orderedElements.Where(x => x != null).Count() == 4)
    //            {
    //                int index = orderedElements.ToList().FindIndex(x => x == null);
    //                orderedElements[index] = elements.Except(orderedElements.Where(x => x != null)).FirstOrDefault();
    //            }

    //            //Color?[] assignedColors = elements.Select(x => x.color).ToArray();
    //            //if(assignedColors.Count(x => x == null) == 1)
    //            //    Color?[] allColors = Enum.GetValues(typeof(Color)).Cast<Color?>().ToArray();
    //            //    Color?[] resultColors = allColors.Except(assignedColors).ToArray();
    //            //    if (resultColors.Count() == 1)
    //            //    {
    //            //        elements.FirstOrDefault(x => x != null && x.color == null).color = resultColors[0];
    //            //    }
    //            //}

    //            //Nationality?[] assignedNationality = elements.Select(x => x.nationality).ToArray();
    //            //if (assignedNationality.Count(x => x == null) == 1)
    //            //{
    //            //    Nationality?[] allNationalities = Enum.GetValues(typeof(Nationality)).Cast<Nationality?>().ToArray();
    //            //    Nationality?[] resultNationalities = allNationalities.Except(assignedColors).ToArray();
    //            //    if (resultNationalities.Count() == 1)
    //            //    {
    //            //        elements.FirstOrDefault(x => x != null && x.nationality == null).nationality = resultNationalities[0];
    //            //    }
    //            //}

    //        }


    //        return elements;
    //    }


    //    public static Nationality DrinksWater()
    //    {
    //        Solve();
    //        return (Nationality) orderedElements.FirstOrDefault(x => x.drink == Drink.Water).nationality;
    //    }

    //    public static Nationality OwnsZebra()
    //    {
    //        Solve();
    //        return (Nationality)orderedElements.FirstOrDefault(x => x.pet == Pet.Zebra).nationality;
    //    }
    //}





    // 2º MÉTODO

    public enum Color { Red, Green, Ivory, Yellow, Blue }
    public enum Nationality { Englishman, Spaniard, Ukrainian, Japanese, Norwegian }
    public enum Pet { Dog, Snails, Fox, Horse, Zebra }
    public enum Drink { Coffee, Tea, Milk, OrangeJuice, Water }
    public enum Activity { Dancing, Reading, Football, Chess, Painting }

    public static class ZebraPuzzle
    {
        private class element
        {
            public Color? color;
            public Nationality? nationality;
            public Pet? pet;
            public Drink? drink;
            public Activity? activity;
            public int position = -1;

            public element()
            { }

            public element(element e)
            {
                color = e.color;
                nationality = e.nationality;
                pet = e.pet;
                drink = e.drink;
                activity = e.activity;
                position = e.position;
            }
        }

        // Funciones para establecer los elementos
        private static void SetColor(element e, Color? newColor) { e.color = newColor; }
        private static void SetNationality(element e, Nationality? newNationality) { e.nationality = newNationality; }
        private static void SetPet(element e, Pet? newPet) { e.pet = newPet; }
        private static void SetDrink(element e, Drink? newDrink) { e.drink = newDrink; }
        private static void SetActivity(element e, Activity? newActivity) { e.activity = newActivity; }

        //1. There are five houses.
        private static element?[] elements = new element[5];
        private static element?[] orderedElements = new element[5];

        private static element?[] constraints = new element[] {
            new element() {color = Color.Green,drink = Drink.Coffee},
            new element() {pet = Pet.Snails, activity = Activity.Dancing},
            new element() {color = Color.Yellow, activity = Activity.Painting},
            new element() {position = 2, drink = Drink.Milk},
            new element() {activity = Activity.Football,drink = Drink.OrangeJuice}
        };

        private static bool IsValidSolution(element[]? solution){
            foreach (element solutionElement in solution)
            {
                if (solutionElement.color == null || 
                    solutionElement.pet == null ||
                    solutionElement.activity == null || 
                    solutionElement.nationality == null ||
                    solutionElement.drink == null ||
                    solutionElement.position == -1) return false;
            }
            return true;
        }

        private static bool CheckSolution(element[]? solution)
        {
            bool isValidSolution = true;
            foreach (element solutionElement in solution)
            {
                foreach (element constraint in constraints)
                {
                    int numMatches = 0;
                    if (solutionElement.color != null && constraint.color != null)
                    {
                        if (solutionElement.color.Equals(constraint.color)) numMatches++;
                        else if (numMatches > 0) return false;
                    }
                    if (solutionElement.nationality != null && constraint.nationality != null)
                    {
                        if (solutionElement.nationality.Equals(constraint.nationality)) { numMatches++; }
                        else if (numMatches > 0) return false;
                    }
                    if (solutionElement.pet != null && constraint.pet != null)
                    {
                        if (solutionElement.pet.Equals(constraint.pet)) { numMatches++; }
                        else if (numMatches > 0) return false;
                    }
                    if (solutionElement.drink != null && constraint.drink != null)
                    {
                        if (solutionElement.drink.Equals(constraint.drink)) { numMatches++; }
                        else if (numMatches > 0) return false;
                    }
                    if (solutionElement.activity != null && constraint.activity != null)
                    {
                        if (solutionElement.activity.Equals(constraint.activity)) { numMatches++; }
                        else if (numMatches > 0) return false;
                    }
                    if (solutionElement.position != -1 && constraint.position != -1)
                    {
                        if (solutionElement.position.Equals(constraint.position)) { numMatches++; }
                        else if (numMatches > 0) return false;
                    }
                    //Add position constraints
                    //The green house is immediately to the right of the ivory house.
                    if (solutionElement.color == Color.Ivory && solutionElement.position != -1)
                    {
                        element greenHouse = solution.FirstOrDefault(x => x.color == Color.Green);
                        if (greenHouse != null && greenHouse.position != -1)
                        {
                            if (!(solutionElement.position == greenHouse.position - 1)) return false;
                        }
                    }
                    //The person who enjoys reading lives in the house next to the person with the fox.
                    if (solutionElement.pet == Pet.Fox && solutionElement.position != -1)
                    {
                        element readingEnjoyer = solution.FirstOrDefault(x => x.activity == Activity.Reading);
                        if (readingEnjoyer != null && readingEnjoyer.position != -1)
                        {
                            if (!( Math.Abs(readingEnjoyer.position - solutionElement.position) == 1)) return false;
                        }
                    }
                    //The painter's house is next to the house with the horse.
                    if (solutionElement.activity == Activity.Painting && solutionElement.position != -1)
                    {
                        element horsePet = solution.FirstOrDefault(x => x.pet == Pet.Horse);
                        if (horsePet != null && horsePet.position != -1 )
                        {
                            if (!( Math.Abs(horsePet.position - solutionElement.position) == 1)) return false;
                        }
                    }
                    //The Norwegian lives next to the blue house.
                    if (solutionElement.nationality == Nationality.Norwegian && solutionElement.position != -1)
                    {
                        element houseColor = solution.FirstOrDefault(x => x.color == Color.Blue);
                        if (houseColor != null && houseColor.position != -1)
                        {
                            if (!(Math.Abs(houseColor.position - solutionElement.position) == 1)) return false;
                        }
                    }

                }
            }
            return isValidSolution;
        }

        private static element[]? AddColors(Color[] colorsToAdd, element[]? currentSolution)
        {
            if (!CheckSolution(currentSolution)) return null;    //Comprueba que se cumplan todas las restricciones. Si no se cumplen termina con esta rama.
            if (colorsToAdd.Length == 0)
            {
                element[]? solution = AddPets(new Pet[] {Pet.Horse,Pet.Zebra,Pet.Snails,Pet.Fox},currentSolution);
                if (solution != null && IsValidSolution(solution)) return solution;
            }
            else
            {
                
                for (int i = 0; i < currentSolution.Length; i++)
                {
                    //element[]? newSolution = (element[]?)currentSolution.Clone();
                    element[]? newSolution = new element[5];
                    for (int j = 0; j < currentSolution.Length; j++)
                    {
                        newSolution[j] = new element(currentSolution[j]);
                    }
                    //currentSolution.CopyTo(newSolution, 0);
                    element newSolutionElement = newSolution[i];
                    if (newSolutionElement.color == null)
                    {
                        newSolutionElement.color = colorsToAdd[0];
                        var opopo = colorsToAdd.Skip(1).ToArray();
                        element[]? solution = AddColors(colorsToAdd.Skip(1).ToArray(), (element[]?)newSolution.Clone());
                        if (solution != null && IsValidSolution(solution)) return solution;
                    }
                    if (IsValidSolution(newSolution)) return newSolution;   //Esta función solo sería necesaria en el último caso
                }
            }
            return null;
        }

        private static element[]? AddPets(Pet[] petsToAdd, element[]? currentSolution)
        {
            if (!CheckSolution(currentSolution)) return null;    //Comprueba que se cumplan todas las restricciones. Si no se cumplen termina con esta rama.
            if (petsToAdd.Length == 0)
            {
                element[]? solution = AddDrinks(new Drink[] {Drink.OrangeJuice,Drink.Coffee,Drink.Water,Drink.Milk},currentSolution);
                if (solution != null && IsValidSolution(solution)) return solution;
            }
            else
            {

                for (int i = 0; i < currentSolution.Length; i++)
                {
                    element[]? newSolution = new element[5];
                    for (int j = 0; j < currentSolution.Length; j++)
                    {
                        newSolution[j] = new element(currentSolution[j]);
                    }
                    element newSolutionElement = newSolution[i];
                    if (newSolutionElement.pet == null)
                    {
                        newSolutionElement.pet = petsToAdd[0];
                        element[]? solution = AddPets(petsToAdd.Skip(1).ToArray(), (element[]?)newSolution.Clone());
                        if (solution != null && IsValidSolution(solution)) return solution;
                    }
                    if (IsValidSolution(newSolution)) return newSolution;   //Esta función solo sería necesaria en el último caso
                }
            }
            return null;
        }

        private static element[]? AddDrinks(Drink[] drinksToAdd, element[]? currentSolution)
        {
            if (!CheckSolution(currentSolution)) return null;    //Comprueba que se cumplan todas las restricciones. Si no se cumplen termina con esta rama.
            if (drinksToAdd.Length == 0)
            {
                element[]? solution = AddActivities(new Activity[] {Activity.Reading,Activity.Dancing,Activity.Painting,Activity.Football},currentSolution);
                if (solution != null && IsValidSolution(solution)) return solution;
            }
            else
            {

                for (int i = 0; i < currentSolution.Length; i++)
                {
                    element[]? newSolution = new element[5];
                    for (int j = 0; j < currentSolution.Length; j++)
                    {
                        newSolution[j] = new element(currentSolution[j]);
                    }
                    element newSolutionElement = newSolution[i];
                    if (newSolutionElement.drink == null)
                    {
                        newSolutionElement.drink = drinksToAdd[0];
                        element[]? solution = AddDrinks(drinksToAdd.Skip(1).ToArray(), (element[]?)newSolution.Clone());
                        if (solution != null && IsValidSolution(solution)) return solution;
                    }
                    if (IsValidSolution(newSolution)) return newSolution;   //Esta función solo sería necesaria en el último caso
                }
            }
            return null;
        }

        private static element[]? AddActivities(Activity[] activitiesToAdd, element[]? currentSolution)
        {
            if (!CheckSolution(currentSolution)) return null;    //Comprueba que se cumplan todas las restricciones. Si no se cumplen termina con esta rama.
            if (activitiesToAdd.Length == 0)
            {
                element[]? solution = AddPositions(new int[] {1,2,3,4},currentSolution);
                if (solution != null && IsValidSolution(solution)) return solution;
            }
            else
            {

                for (int i = 0; i < currentSolution.Length; i++)
                {
                    element[]? newSolution = new element[5];
                    for (int j = 0; j < currentSolution.Length; j++)
                    {
                        newSolution[j] = new element(currentSolution[j]);
                    }
                    element newSolutionElement = newSolution[i];
                    if (newSolutionElement.activity == null)
                    {
                        newSolutionElement.activity = activitiesToAdd[0];
                        element[]? solution = AddActivities(activitiesToAdd.Skip(1).ToArray(), (element[]?)newSolution.Clone());
                        if (solution != null && IsValidSolution(solution)) return solution;
                    }
                    if (IsValidSolution(newSolution)) return newSolution;   //Esta función solo sería necesaria en el último caso
                }
            }
            return null;
        }

        private static element[]? AddPositions(int[] positionsToAdd, element[]? currentSolution)
        {
            if (!CheckSolution(currentSolution)) return null;    //Comprueba que se cumplan todas las restricciones. Si no se cumplen termina con esta rama.

            if (positionsToAdd.Length == 0)
            {
                return currentSolution;
            }
            else
            {
                for (int i = 0; i < currentSolution.Length; i++)
                {
                    element[]? newSolution = new element[5];
                    for (int j = 0; j < currentSolution.Length; j++)
                    {
                        newSolution[j] = new element(currentSolution[j]);
                    }
                    element newSolutionElement = newSolution[i];
                    if (newSolutionElement.position == -1)
                    {
                        newSolutionElement.position = positionsToAdd[0];
                        element[]? solution = AddPositions(positionsToAdd.Skip(1).ToArray(), (element[]?)newSolution.Clone());
                        if (solution != null && IsValidSolution(solution) && CheckSolution(solution)) return solution;
                    }
                    if (IsValidSolution(newSolution)) return newSolution;   //Esta función solo sería necesaria en el último caso
                }

                return null;
            }
        }

        private static void Solve()
        {
            #region Initial Solution
            // Creamos una solución inicial que contenga todas las nacionalidades y los constrains asociados a ellas.
            elements[0] = new element() { nationality = Nationality.Englishman,color = Color.Red };
            elements[1] = new element() { nationality = Nationality.Spaniard, pet = Pet.Dog };
            elements[2] = new element() { nationality = Nationality.Norwegian, position = 0 };
            elements[3] = new element() { nationality = Nationality.Ukrainian, drink = Drink.Tea };
            elements[4] = new element() { nationality = Nationality.Japanese, activity = Activity.Chess };
            #endregion

            element[]? possibleSolution = AddColors(new Color[] { Color.Ivory, Color.Blue, Color.Green, Color.Yellow }, elements);

            if (IsValidSolution(possibleSolution)) orderedElements = possibleSolution;
        }


        public static Nationality DrinksWater()
        {
            Solve();
            return (Nationality)orderedElements.FirstOrDefault(x => x.drink == Drink.Water).nationality;
        }

        public static Nationality OwnsZebra()
        {
            Solve();
            return (Nationality)orderedElements.FirstOrDefault(x => x.pet == Pet.Zebra).nationality;
        }
    }
}
