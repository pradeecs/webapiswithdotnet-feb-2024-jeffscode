# Issue Tracker API

The Company is "Hypertheory, Inc"

Supported list of software.

For that supported list of software, *employees* can issue a support request.

These support requests will be routed to the appropriate desktop support person for follow-up. (More on that in Microservices class, but we'll touch on it)

What they need:

- They need the ID of the employee making the request
- They need the software the issue is with, along with the version (the desktop support people maintain this list in their API.)
- They need a brief description of the issue (Maximum 1024 characters)
- When the issue was filed (Date and Time)



## Three Vectors of API Design

- The Resource
    - "An important thingy we want to expose to our users"
- The Method
    - GET - I would like the current representation of this resource.
    - POST - 
    - PUT
    - DELETE
    - HEAD
    - others that you should be suspect of using.
- The Representation

```http
POST /software/{id}/issues
Content-Type: application/json
Authorization: "something here that identified the user"

{

    "description": "Done broke"
   
}
```

POST /software/e42171ef-6faa-4071-9dd8-85370e8e20ed/issues

{
    "description": "Bad Stuff"
}

GET /software/e42171ef-6faa-4071-9dd8-85370e8e20ed/issues

GET /software/issues
GET /software-issues



```http
200 Ok
Content-Type: application/json

{
    "id": "839839839guid",
    "description": "Done Broke",
    "software": "Excel",
    "status": "Pending"
}

```


Http Methods:

Method  Cacheable   Safe    Idempotent
GET     Yes         Yes     Yes
POST    No*         No      No
PUT     No          No      Yes
DELETE  No          No      Yes


DELETE from Customers where CustomerId = 'Bob-Smith'

Cacheable - clients can hold on to the representations according to the cache-control header.
Safe - No "Side effects" - Stuff the "business" cares about.
        - for example, NEVER EVER EVER do something like make it so a user of your API can delete a policy by doing a GET request.

Idempotent - doing it multiple times is "Safe" - it has the same response.

GET /fireemployees?id=39893893


GET /checkout

Item    Description Price   Action
1       Eggs        3.99     [Remove] <a href="checkout?remove-item=1">Remove</a>
13      Beer        14.99    [Remove] <a href="checkout?remove-item=1">Remove</a>


[Buy Now]