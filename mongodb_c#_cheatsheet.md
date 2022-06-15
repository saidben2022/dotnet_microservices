# MongoDB C# Driver Cheat Sheet
*(C) 2015 by [Derek Hunziker](http://www.layerworks.com), (C) 2017 by [AppsOn](https://blog.appson.tech)*

As of releasing MongoDB 3.4 and C# Driver v2.4, original cheatsheet by Derek is outdated. In addition, it has some deficiencies like connecting to MongoDB, creating indexes, etc.
This updated version works fine with C# Driver v2.4.7 and MongoDB v3.4.

## Setup

### Define Document Models
*Note:* Defined models and collections will be used in entire cheatsheet.
```csharp
[BsonIgnoreExtraElements]
public class User {
    [BsonId]
    public int Id { get; set } // It's better to use ObjectId, we used int for simplicity
    public int Age { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public List<string> AliasNames { get; set; }
    public List<Account> Accounts { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime ModifiedOn {get; set; }
}
```

### Connect To Database
```csharp
// Default connection, localhost:27017
var db = MongoClient();

// With Connection String
var db = MongoClient("http://localhost:27017");

// With MongoUrl Builder
var url = MongoUrl.Create("http://localhost:27017");
var db = MongoClient(url);
```

### Get Collection
```csharp
var collection = db.GetCollection<User>("users");
```

## Insert
### Basic Insert
```csharp
var user = new User
{ 
    Name = "Hossein",
    Email = "hossein@example.com"
};

// Basic Insert
collection.InsertOne(user);

// With Write Concern
collection.WithWriteConcern(WriteConcern.WMajority).InsertOne(user);
```

### Insert w/ ID
```csharp
var user = new User {
    Id = 10,
    Name = "Hossein",
    Email = "hossein@example.com"
}
collection.InsertOne(user);
```

### Insert Nested
```csharp
var user = new User {
    Name = "Hossein",
    Email = "hossein@example.com",
    Address = new Address {
        City = "Portland",
        State = "OR",
        Zip = "97232"
    }
}
collection.InsertOne(user);
```
### Bulk Insert
```csharp
var users = new []
{
    new User { Name = "Danial", Email = "danial@example.com" },
    new User { Name = "Bahar", Email = "bahar@example.com" },
    new User { Name = "Shadi", Email = "shadi@exmaple.com" }
};
collection.InsertMany(users);
```

## Filters
Find, update and removing documents use filters, which can be made by filter definition builder.
```csharp
var builder = Builders<User>.Filter;
```

### Basic Filter
```csharp
// Empty Filter (matches all)
var empty = builder.Empty;

// Filter by field
var idFilter = builder.Eq(u => u.Id, 10);

// Filter by field with list of desired values
var idListFilter = builder.In(u => u.Id, new [] { 10, 14 });
```
You can use `Gt` (<), `Lt` (>), `Ne` (!=), `Lte` (<=), `Gte` (>=) filters, just like `Eq` (==).

### Embedded Document/Array Filter
```csharp
// Filter by embedded document's field
var stateFilter = builder.Eq(u => u.Address.State, "OR");

// Filter by array element
var aliasFilter = builder.AnyEq(u => u.AliasNames, "a3dho3yn");

// Filter by array element's field
var accountFilter = builder.ElemMatch(u => u.Accounts, acc => acc.Provider == "GitHub");

// Filter by array element in desired values
var aliasFilter = builder.AnyIn(u => u.AliasNames, new [] { "a3dho3yn", "hossein" });

// Filter by all array elements
var aliasFilterAll = builder.All(u => u.AliasNames, new [] { "a3dho3yn", "hossein" });
```
There are AnyXx modifiers, where Xx is Gt, Lt, ... (just like single field filters).

### Field Properties Filter
```csharp
// Filter by field existance
var hasNameFieldFilter = builder.Exists(u => u.Name);

// Filter by field type
var idTypeFilter = builder.Type(u => u.Id, BsonType.ObjectId);
```

### Regular Expression Filter
```csharp
// Filter with regular expression
var pattern = new BsonRegularExpression("yn$");
var nameFilter = builder.RegEx(u => u.Name, pattern);
```

### Combine Filters
You can combine filters with bitwise operators (e.g. `&`, `|`) or use builders' methods.
```csharp
var idAndStateFilter = builder.And(new [] {idFilter, stateFilter}); // == idFilter & stateFilter
var idOrStateFilter = builder.Or(new [] {idFilter, stateFilter}); // == idFilter | stateFilter
```

## Find
### Basic Find
Find command will return an iterator, which can be used to retrieve documents.
```csharp
var cursor = collection.Find(stateFilter);

// Find One
var user = cursor.FirstOrDefault();

// Find All
List<User> users = cursor.ToList();
```

### Sort, Skip, Limit
We use sort definition builder to define sort order.
```csharp
var sort = Builders<User>.Sort.Ascending(u => u.Name).Descending(u => u.Age);
var cursor = collection.Find(filter).Sort(sort).Skip(5).Limit(10);
```

### Projection
We use porojection definition builder to define how documents will be represented.
```csharp
var builder = Builders<User>.Projection;
var project = builder.Include(u => u.Name).Exclude(u => u.Id);
var cursor = collection.Find(filter).Project(project);
```
An alternative way to make projections is using Expressions: `builder.Expression(u => new { u.Name });`.

## Update
Update, needs a filter definitin to find document and an update definition to define which fields and how will change.

Updates can affect one or many documents:
```csharp
var cursor = collection.UpdateOne(filter, update);
var cursor = collection.UpdateMany(filter, update);
```

### Update Definition
```csharp
var builder = Builders<User>.Update;
```
#### Basic Updates
```csharp
// Set field
var setName = builder.Set(u => u.Name, "Hossein");

// Unset field
var unsetName = builder.UnSet(u => u.Name);

// Increment field
var incAge = builder.Inc(u => u.Age);

// Current Date
var updateModification = builder.CurrentDate(x => x.ModifiedOn);
```
There are Mul(tiply), BitwiseAnd, BitwiseOr and BitwiseXor that can be used just like Set operator.

#### Min/Max
Min and Max operators will modify fields toward min/max value. So, they will update field *only if* it's value is respectivele greater/less than provided value.
```csharp
var min = builder.Min(u => u.Age, 18) // Updates field if Age > 18
var max = builder.Max(u => u.Age, 18) // Updates field if Age < 18
```

#### Rename Field
```csharp
var rename = builder.Rename(u => u.Id, "UserId");
```

#### Update Array Fields
```csharp
// Push
var push = builder.Push(u => u.AliasNames, "a3d");

// Pop
var pop = builder.Pop(u => u.AliasNames);

// Pull: removes elements matching value
var pull = builder.Pull(u => u.AliasNames, "a3d");

// Pull All: removes elements matches any values in list
var pullAll = builder.PullAll(u => u.AliasNames, new [] { "a3dho3yn", "hossein" });

// Add To Set
var addToSet = builder.AddToSet(u => u.AliasNames, "a3d");
```
AddToSet, deals with arrays as sets, and adds elements to an array *only if* it do not already exist.

#### Set On Insert
Provided value only used on insert. Then, setting upsert flag and using SetOnInsert is equal to *Find Or  Create*.
```csharp
var setOnInsert = builder.SetOnInsert(u => u.Name, "Farid");
```

### Upsert
Upsert, will _insert_ new document if it fails in finding a document.
```csharp
var options = new UpdateOptions() {IsUpsert = true};
collection.UpdateOne(filter, update, options);
```

### Replace (Wholesale Update)
```csharp
var user = new User() {
    Id = 10,
    Name = "Hamed",
    Email = "hamed@example.com",
};
var filter = Builders<User>.Filter.Eq(u => u.Id, 10);
collection.ReplaceOne(filter, user);
```

## Remove
### Basic Remove
```csharp
collection.DeleteMany(filter);
```

### Remove One
```csharp
collection.DeleteOne(filter);
```