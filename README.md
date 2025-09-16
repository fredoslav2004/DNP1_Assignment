# DNP1_Assignment
# VIA UC STE DNP1 Assignment Repository

## Assignment 1

We need a **User** entity with at least a username and a password. It must have an Id of type `int`.

We need a **Post** entity written by a User. It contains a Title and a Body, and must have an Id of type `int`.

A User can write a **Comment** on a Post. A Comment contains a Body and must have an Id of type `int`.

All entities must have an Id of type `int`.

---

Based on the above requirements, create a domain model diagram showing:

- The entities of the system [✓]
- The properties (attributes) on the entities [✓]
- The relationships between entities (e.g., Post is written by a User). Include multiplicities at both ends, as taught in Entity Relationship Diagrams (DBS) or Domain Models (SWE). [✓]

## Assignment 2

### Must-Have Requirements

- Create a new user (username, password, etc.) [✓]
- Create a new post (title, body, user ID) [✓]
- Add a comment to an existing post (body, user ID, post ID) [✓]
- View posts overview (display [title, ID] for each post) [✓]
- View a specific post (title, body, and comments on the post) [✓]

## Assignment 3

In your new project, implement each repository interface as follows:

Do not use a private field variable for a list of entities. Instead, represent the "list" as a file.

Each method should:

- Read the JSON text from the file.
- Deserialize the JSON into a list.
- Interact with the list (e.g., add, retrieve, delete, or overwrite entities).
- Serialize the list back to JSON.
- Write the JSON back to the file, overwriting existing content.

Use async file interaction methods like `ReadAllTextAsync` and `WriteAllTextAsync`.
