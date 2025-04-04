Why do we use Repositories ?
Because it takes away the task of communicating with the db from us.

But what if there are 100 of entities ? Do we create 100 Repositories ?
We'll create Generic Repository, using Specification Pattern.

An example of Generic Repository
![image](https://github.com/user-attachments/assets/bad7ed35-8b67-44c4-bb66-1655fccca0c9)

It can only be used for entities that are derived from 'BaseEntity'

But this introduces anti-pattern problem.

What's that problem?
Let say we have an entity 'Product' and a property of it 'Price' which is specific to this entity only.
How do we query product based on price using generic repository ?

We can use Specification Pattern, to be exact Generic Expression.

//Put image here

This returns an IQueryable object, which exposes the db.
And it allows us to create inefficient queries.
What it is doing is creating a LeakyAbstraction.

The whole point of Repository Pattern is to abstract all these.

So What do we do now?

The Specification pattern to the rescue!
    -Describes a query in an object
    -Returns an IQueryable<T>
    -Generic List method takes specification as parameter
    -Specification can have meaningful name
        -OrdersWithItemsAndSortingSpecification
        (This has meaningful name,, a quick glance at code describes what its going to do)

Flow:
spec -> Evaluator -> IQueryable<T> -> ListAsync(spec)

//Projection using specification pattern
    -we can pass select queries to get the columns we need
    -for this we need to create similar methods of Specification class but with <T,TResult> types.
    -and handle same in specification evaluator

//TODO: remove filters feature
//TODO: search debounce for products
//TODO: Rate limiting

------------------------------------------------ANGULAR---------------------------------------------------
Interceptors:
will be used to put some logic before our http requests goes out, or after http responses comes into our app.

return next(req); put your logic before/after this as per your need (before for before req goes out, after for after response comes in)

Signals:
State that can be observed and reacted to
Provides clean API for state management
Avoid the complexity of observables
Cant use signals for asynchronous functions

we have 3 main types:
-Signal: get,set,update the value
-Computed: this computed value changes as the signal changes
-Effect: it can react to a signal updating

(click)="$event.stopPropagation()"
^This is used to stop propagating button click to the card (inside which the button lies) which also has a click event.

