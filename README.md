# DNP1_Assignment
VIA UC STE DNP1 Assignment repo

# Assignment 1

We need a User, having at least a username and a password. It needs an Id of type int.
We need a Post. It’s written by a User. It contains a Title and a Body. It also needs an Id of type int.
A User can also write a Comment on a Post. A Comment contains a Body, and an Id of type int.

All entities must have an Id of type int.

---

Based on the above requirements, you must create a domain model diagram, where we can see:

- The entities of the system [✓]
- The properties (attributes) on the entities [✓]
- The relationships between entities, e.g. Post is written by a User. Remember multiplicities at both ends, like you were taught for the Entity Relationship Diagram in DBS, or the Domain Model in SWE. [✓]

# Assignment 2

## Must-have requirements:

- Create new user (user name, password, etc) [✓]
- Create new post (title, body, user id) [✓]
- Add comment to existing post (body, user id, post id) [✓]
- View posts overview (just display [title, id] for each post) [✓]
- View specific post (see title and body, and comments on the post) [✓]