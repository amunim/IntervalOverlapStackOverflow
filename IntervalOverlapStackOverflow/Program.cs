//itemId: qty
Dictionary<int, long> itemsQts =
    Enumerable.Range(1, 1000000)
    .ToDictionary(key => key, elem => (long)300);
//.ToDictionary(key => key, elem => (long)Random.Shared.Next(1000));

List<long> Batches = new()
{
    500, //should return: 300, 200 
    200, //should return: 100, 100
    400, //should return: 200, 200
};

//for SQL SUM between unbound preceeding and 1 preceeding
long preceedingRunningSum = 1;
var q1 = (
    from item in itemsQts
    select new
    {
        itemId = item.Key,
        qty = item.Value,
        lowBound = preceedingRunningSum,
        highBound = preceedingRunningSum += item.Value
    }).ToList();

//for SQL SUM between unbound preceeding and 1 preceeding
long preceedingRunningSum2 = 1;
var q2 = (
    from batch in Batches
    select new
    {
        qty = batch,
        lowBound = preceedingRunningSum2,
        highBound = (preceedingRunningSum2 += batch),
    }).ToList();

//for SQL inner join on range(like in where claus below)
var batchMatch =
    from batch in q2
    from item in q1
    where item.highBound >= batch.lowBound &&
          batch.highBound >= item.lowBound
    orderby 1, 2, 3
    select new
    {
        batchQty = batch.qty,
        itemId = item.itemId,
        itemQty = Math.Min(batch.highBound, item.highBound) - Math.Max(batch.lowBound, item.lowBound),
    };

Console.WriteLine("batchqty,itemId,itemQty");
foreach (var x in batchMatch)
{
    Console.WriteLine($"{x.batchQty},\t{x.itemId},\t{x.itemQty}");
}
Console.Read();