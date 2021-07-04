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

## ISessionFactory Configuration

Configuration and SessionFactory:
- An instance of `NHibernate.Cfg.Configuration` represents an entire set of mappings of an application's .NET types to a SQL database.
- When all mappings have been parsed by the Configuration, the application must obtain a factory for ISession instances: `ISessionFactory sessions = cfg.BuildSessionFactory();`
- (We can have multiple ISessionFactory if we have multiple databases).

User provided ADO.NET connection:
- An ISessionFactory may open an ISession on a user-provided ADO.NET connection:
	```C#
	var conn = myApp.GetOpenConnection();
	var session = sessions.OpenSession(conn);
	```
Settings for ADO.NET connection: https://nhibernate.info/doc/nhibernate-reference/session-configuration.html#configuration-hibernatejdbc

All other configurations: https://nhibernate.info/doc/nhibernate-reference/session-configuration.html

## Mapping

Collection mapping:
- NHibernate requires that persistent collection-valued fields be declared as a generic interface type.
- cascade: https://ayende.com/blog/1890/nhibernate-cascades-the-different-between-all-all-delete-orphans-and-save-update
- cascade in removing context, for example in CatStore:
	+ normal case: remove a CatStore instance will NOT remove all associated cats.
	+ delete: remove a CatStore instance will remove all associated cats.
	+ delete: remove a cat from CatStore associated collection of cats will set cat's foriegn key to null (orphan)
	+ all-delete-orphan: remove a cat from CatStore associated collection of cats will remove the cat if it's orphan
- fetch:
	+ select: lazy loading the collection
	+ join: eager loading the collection by left join query
	+ subselect: when access a collection, fetch all collections using a sub-query (e.g. `select ... from posts where blogId in (select top(5) Id from blogs)`)
- inverse: in bidirectional associations, to tell the non-inversed side to have the responsible of persisting the data. By default, both sides will persist twice if we call persisting twice.
- batch-size: loading n bunch of collections to reduce n+1 issue. E.g. for each n blogs, loadings posts will produce n+1 queries, but if using batch-size, it will produce n/batch-size + 1 queryies.

Mapping is often used:
- Tags: class, property, id, many-to-one, bag, one-to-many.

Attributes to notice:
- `lazy` (true/false): in set/bag, specify loading the collection when getting the entity or when we calling the collection property.

## Open points

