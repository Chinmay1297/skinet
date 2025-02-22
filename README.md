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

We can use Specific Pattern, to be exact Generic Expression.
