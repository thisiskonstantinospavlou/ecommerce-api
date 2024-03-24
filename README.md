# Solution
* This is a solution for the FunBooksAndVideos e-commerce shop. This solution is by far not ready for production, however it tries to follow closely to requirements set.
* The solution runs in .NET 8 with Entity Framework - SQLite, NUnit and Moq.
* A high level design of the database created can be found [here](./FunBooksAndVideos-db.png).
* The solution has been tested and can run for happy paths, when Visual Studio runs as an Administration due to the creation of an SQLite database in C:.
* This solution has been created within a weekend and therefore it was time restricted.

## Assumptions, Improvements and Future Work
* It is unclear from the requirements whether physical products can be books, videos or both. It has been assumed that all products can be physical or electronic.
* The Solution is split into layers that are represented by Projects.
* All internal IDs should not be exposed. Instead GUIDs should be used publicly that are not the Primary Keys from the database.
* Eception handling would need improvement. More exceptions needed and correct matching in the try/catch statements. Correct exceptions with the HTTP Code should be returned too.
* Validators for the validation in the spec, should ideally run in the Domain.Logic layer. Depending on the validation needed, possibly directly on the controller.
* Unit Testing should cover the remaining projects.
* The Data.EF layer would need some changes to ensure all possible exception scenarios are handled.
* The Data.EF Conext may need some rework to match IDs more appropriately.
* Additional work is required for the generation of appropriate responses or running other processes elsewhere when a Shipping slip is created. Currently only the address is returned as a result.
* Better separation of objects through layers should be considered. Especially between the Controllers and Domain.Logic layers.
* Automapper has been used for mapping between Api.Models and Data.EF, however DTO objects could also be used # Solution
* This is a solution for the FunBooksAndVideos e-commerce shop. This solution is by far not ready for production, however it tries to follow closely to requirements set.
* The solution runs in .NET 8 with Entity Framework - SQLite, NUnit and Moq.
* The solution has been tested and can run for happy paths, when Visual Studio runs as an Administration due to the creation of an SQLite database in C:.
* This solution was created within a weekend and therefore it was time restricted.

## Assumptions, Improvements and Future Work
* It is unclear from the requirements whether physical products can be books, videos or both. It has been assumed that all products can be physical or electronic.
* The Solution is split into layers that are represented by Projects
* All internal IDs should not be exposed. Instead GUIDs should be used publicly that are not the Primary Keys from the database.
* Eception handling would need improvement. More exceptions needed and correct matching in the try/catch statements. Correct exceptions with the HTTP Code should be returned too.
* Validators for the validation in the spec, should ideally run in the Domain.Logic layer. Depending on the validation needed, possibly directly on the controller.
* Unit Testing would should cover the remaining projects
* The Data.EF layer would need some changes to ensure all possible exception scenarios are handled
* The Data.EF Conext may need some rework to match IDs more appropriately
* Additional work is required for the generation of appropriate responses or running other processes elsewhere when a Shipping slip is created. Currently only the address is returned as a result.
* Better separation of objects through layers should be considered. Especially between the Controllers and Domain.Logic layers.
* Automapper has been used for mapping between Api.Models and Data.EF, however DTO objects could also be used explicitely.

## A path that can be followed in order:

### 1. New video membership, and Book when no customer has previously no membership throws exception

```

{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "video",
      "type": 1

    },{
      "name": "The Girl on the train",
      "type": 3

    }
  ]
}
```

### 2. New premium membership, and physical Book succeed with address

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "premium",
      "type": 1

    },{
      "name": "The Girl on the train",
      "type": 3

    }
  ]
}
```

### 3. Subsequently, electronic Book succeeds without address

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "Comprehensive First Aid Training",
      "type": 3
    }
  ]
}
```

### 4. Changing membership to Book succeeds

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "Book",
      "type": 1
    }
  ]
}
```

### 5. Now, electronic Video throws exception

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "Attack On Titan, Volume 1",
      "type": 2
    }
  ]
}
```

### 6. Electronic Video, and Book membership also throws

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "Attack On Titan, Volume 1",
      "type": 2
    },
    {
      "name": "Book",
      "type": 1
    }

  ]
}
```

### 7. However, Book only succeeds

```
{
  "purchaseOrderId": 1,
  "customerId": 2,
  "total": 32,
  "items": [
    {
      "name": "Comprehensive First Aid Training",
      "type": 3
    }

  ]
}
```