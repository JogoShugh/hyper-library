Hyper-Library
=============

A small sample used to illustrate some of the benefits of including links inside your next ASP.NET WebAPI implementation.

The API is simple, it allows you to explore a list of books, check books in, and check them out again.

## Contained Within
A sample library API along with two clients.

1. Api Implementation 
2. A Resource Driven Client 
3. A Hypermedia Driven Client 

## The Scenario 
The API makes its way into the public domain, and a couple of wild clients appear (hark, someone is using our API!).
We begin to celebrate a little too soon, as now the Grand Library Council has decreed that too many people are checking books
out, and never checking them back in. This will not do! 

To combat the ever growing pressures of late returns, the *workflow* of our API must change. Now, users with unpaid fines
(due to late returns) must first *pay* their fines, before checking out any more books.

To simulate the change in this workflow - find the class

```
        InMemoryFineRepository
````

and flip the magic switch 

`````

        /// <summary>
        /// Switch me to 'true' enable fines
        /// </summary>
        private static bool _hasFines = false;
        
`````

## The Outcome 
We have two wild clients out there, both of which were developed prior to the *pay your fines* API change. One of these clients will
break, and one will survive. Who will win?

The Resource Driven Client has baked within it, knowledge of

1. The available URI's
2. The types being returned from the server
3. The workflow of listing books, checking books out, and returning them

It has no knowledge of paying fines, as this was added after the original client was developed.

The Hypermedia Driven client has baked within it, knowledge of

1. How to find links in a response from the server
2. How to follow those links

When a new business rule is enforced, the Resource driven client is not able to tolerate those changes, while the Hypermedia 
driven client is. This in a very trivial way, demonstrates the evolvality of the hypermedia client. 

## A Note on this Hypermedia Client
This is not designed to be a complete example on how to implement a hypermedia client! It is trivialised significantly.
It does not describe how to implement a custom media type, nor does it describe the implementation of hypermedia affordances such
as forms. This is a contrived example designed to illustrate a basic evolvability as a property of hypermedia.




