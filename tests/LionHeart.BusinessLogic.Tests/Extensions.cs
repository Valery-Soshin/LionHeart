namespace LionHeart.BusinessLogic.Tests;

public static class Extensions
{
    public static void ReplacePropertyValue<TFirstModel, TSecondModel>(this List<TFirstModel> firstCollection,
                                                                       List<TSecondModel> secondCollections,
                                                                       Action<TFirstModel, TSecondModel> action)
    {
        if (firstCollection.Count == 0 || secondCollections.Count == 0)
        {
            throw new ArgumentException("Collection is an empty");
        }
        if (firstCollection.Count != secondCollections.Count)
        {
            throw new ArgumentException("First collection length is not equal to second collection length");
        }
        for (int i = 0; i < firstCollection.Count; i++)
        {
            action(firstCollection[i], secondCollections[i]);
        }
    }
}