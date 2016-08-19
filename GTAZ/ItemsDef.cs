using GTAZ.Inventory;
using GTAZ.Items;

namespace GTAZ
{
    public static class ItemsDef
    {
        public static ItemExample ItemExample = (ItemExample) new ItemExample().SetStackSize(16);

        public static Item[] Items =
        {
            ItemExample
        };

        public static string[] ItemsToStrings(params Item[] items)
        {
            var result = new string[items.Length];
            for (var i = 0; i < items.Length; i++)
            {
                result[i] = items[i].ToString();
            }

            return result;
        }
    }
}
