TinyFactoryGirl
===============

A tiny factory girl to .NET platform based on Ruby Gem FactoryGirl. It just care about store an definition to create some object.

Usage
==============

Store a simple definition
```
var original = new User
{
   Name = "John Due",
   Email = "john.due@example.org"
};

TinyFactoryGirl.Define(() => original);

var user = TinyFactoryGirl.Build<User>();
```

Store a definition with alias
```
TinyFactoryGirl.Define("user", () => new User { Name = "John Due" });
var user = TinyFactoryGirl.Build<User>("user");
```

Override a stored definition
```
TinyFactoryGirl.Define(() => new User { Name = "John Due" });
var user = TinyFactoryGirl.Build<User>("user", _ => _.Email = "john.due@example.org");
```
