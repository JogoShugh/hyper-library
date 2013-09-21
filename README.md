Hyper-Library
=============

A small sample used to illustrate some of the benefits of including links inside your next ASP.NET WebAPI implementation.
The API is simple, it allows you to explore a list of books, check books in, and check them out again. No big deal. 

## Contained Within
A sample library API, along with two API clients.

1) Api Implementation (Link to read me)
2) A Resource Driven Client  (Link to read me)
3) A Hypermedia Driven Client (Link to read me)

## The Scenario 
The API makes its way into the public domain, and a couple of wild clients appear (HARK, someone is using our API!).
We begin to celebrate a little too soon, as now the Grand Library Coucil has decreed that too many people are checking books
out, and never checking them back in. This will not do! 

To combat the ever growing pressures of late returns, the *workflow* of our API must change. Now, users with unpaid fines
(due to late returns) must first *pay* their fines, before checking out any more books.

## The Outcome 
We have two wild clients out there, both of which were developed prior to the *pay your fines* API change. One of these clients will
break, and one will survive. Who will win?

The Resource Driven Client has baked within it, knowlegde of
1) The avalible URI's
2) The types being returned from the sever
3) The workflow of listing books, checking books out, and returning them

It has no knowledge of paying fines, as this was added after the original client was developed. 

The Hypermedia Driven client has baked within it, knowlegde of
1) How to find links in a response from the server
2) How to follow those links

## Note on Hypermedia Clients
This is not designed to be a complete example on how to implement a hypermedia client. 




