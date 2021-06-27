## Getting Started

Notes:
- Mapping can be done either by defining a mapping file (an xml-document) or by decorating the entity with attributes.
- The `SchemaExport` helper class of NHibernate to auto-"magically" generate the schema in the database.
- An `ISessionFactory` is responsible for one database and may only use one XML configuration file (`Web.config` or `hibernate.cfg.xml`).
- An `ISessionFactory` is usually only built once, e.g. at start-up inside `Application_Start`.

## Architecture

Some definitions:
- ISessionFactory: A threadsafe (immutable) cache of compiled mappings for a single database. A factory for ISession and a client of IConnectionProvider. Might hold an optional (second-level) cache of data that is reusable between transactions, at a process- or cluster-level.
- ISession: A single-threaded, short-lived object representing a conversation between the application and the persistent store. Wraps an ADO.NET connection. Factory for ITransaction.  Holds a mandatory (first-level) cache of persistent objects, used when navigating the object graph or looking up objects by identifier.
- Persistent Objects and Collections: Short-lived, single threaded objects containing persistent state and business function. These might be ordinary POCOs, the only special thing about them is that they are currently associated with (exactly one) ISession. As soon as the Session is closed, they will be detached and free to use in any application layer (e.g. directly as data transfer objects to and from presentation).
- Transient Objects and Collections: Instances of persistent classes that are not currently associated with a ISession. They may have been instantiated by the application and not (yet) persisted or they may have been instantiated by a closed ISession.
- ITransaction (NHibernate.ITransaction): A single-threaded, short-lived object used by the application to specify atomic units of work. Abstracts application from underlying ADO.NET transaction. An ISession might span several ITransactions in some cases. Transaction scopes may be used instead.

Instance states:
- transient: The instance is not, and has never been associated with any persistence context. It has no persistent identity (primary key value).
persistent: The instance is currently associated with a persistence context. It has a persistent identity (primary key value) and, perhaps, a corresponding row in the database. For a particular persistence context, NHibernate guarantees that persistent identity is equivalent to CLR identity (in-memory location of the object).
- detached: The instance was once associated with a persistence context, but that context was closed, or the instance was serialized to another process. It has a persistent identity and, perhaps, a corresponding row in the database. For detached instances, NHibernate makes no guarantees about the relationship between persistent identity and CLR identity.



