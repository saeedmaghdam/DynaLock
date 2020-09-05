# DynaLock
> Create and manage locks dynamically in run-time in concurrent software

DynaLock is a library to create and manage locks dynamically in run-time in concurrent software, DynaLock hides all concurrency complexities in distributed systems.
DynaLock currently supports Monitor, Semaphore and AutoResetEvent.

## Important
#### DynaLock is currecntly under development and only supports Monitor, Semaphore and AutoResetEvent.
#### In a very near future, new features will be added to DynaLock
#### Please contribute in development!

## Features
* ### DynaLock's Monitor
	* Create and manage locker objects dynamically in run-time
* ### DynaLock's Semaphore
	* Create and manage semaphore objects dynamically in run-time
* ### DynaLock's AutoResetEvent
	* Create and manage auto reset event objects dynamically in run-time	
* ### DynaLock's Context
	* Create specific work space for different domains to avoid CPU and resource wasting, improve performance, etc.
	* Both DynaLock's Monitor and DynaLock's Semaphore could be injected a context using builder class.
* ### DynaLock's Meta-Data
	* An object that be used to store some info like string values, integers, instances of spesific classes and data structures, so meta-data could be accesses from different threads in current domain.
	* Both DynaLock's Monitor and DynaLock's Semaphore could be injected a meta-data object using property.
	

## Installation

To install DynaLock in Visual Studio's Package Manager Console:

```sh

Install-Package DynaLock -Version 2.2.0

```

To install in a specific project use:

```sh

Install-Package DynaLock -Version 2.2.0 -ProjectName Your_Project_Name

```

To update package use:

```sh

Update-Package DynaLock

```

To update package in a specific project use:

```sh

Update-Package DynaLock -ProjectName Your_Project_Name

```


Or visit DynaLock's [Nuget][nuget-page] page to get more information.

## Usage example

DynaLock is easy to use.

```cs
using (var monitor = new DynaLock.MonitorBuilder()
	.Name("enter_a_unique_name")
	.Build()
){
	...
	monitor.TryEnter();
	...
	if (monitor.IsLockOwned){
		// Some code
	} else {
		// Some code
	}
	...
}
```

And if you've more domain and would like to have individual context per domain, you could use DynaLock like:

```cs
DynaLock.Context.Monitor domain1_context  =  new  DynaLock.Context.Monitor()
DynaLock.Context.Monitor domain2_context  =  new  DynaLock.Context.Monitor()

using (var monitor = new DynaLock.MonitorBuilder()
	.Context(domain1_context)
	.Name("enter_a_unique_name_in_current_domain")
	.Build()
){
	...
	monitor.TryEnter();
	...
	if (monitor.IsLockOwned){
		// Some code
	} else {
		// Some code
	}
	...
}



using (var monitor = new DynaLock.MonitorBuilder()
	.Context(domain2_context)
	.Name("enter_a_unique_name_in_current_domain")
	.Build()
){
	...
	monitor.TryEnter();
	...
	if (monitor.IsLockOwned){
		// Some code
	} else {
		// Some code
	}
	...
}

```

In the above senario, all resources are completely seperated per context and CPU(s) will not be wasted anymore. 

Also, you could find working examples in [DynaLock.Test] project.
  
## Release History
  
### Visit [CHANGELOG.md] to see full change log history of DynaLock

* 2.2.0
	* Added AutoResetEvent to DynaLock
* 2.0.0
	* Added meta-data to contexts
	* Refactored and improved the source code
	* Added comments to methods, properties and classes
* 1.1.0
	* Added Semaphore to DynaLock
* 1.0.0
	* Initialized DynaLock

## Meta
Saeed Aghdam – [Linkedin][linkedin]

Distributed under the MIT license. See [``LICENSE``][github-license] for more information.

[https://github.com/saeedmaghdam/](https://github.com/saeedmaghdam/)

## Contributing

1. Fork it (<https://github.com/saeedmaghdam/DynaLock/fork>)
2. Create your feature branch (`git checkout -b feature/your-branch-name`)
3. Commit your changes (`git commit -am 'Add a short message describes new feature'`)
4. Push to the branch (`git push origin feature/your-branch-name`)

5. Create a new Pull Request

<!-- Markdown link & img dfn's -->

[linkedin]:https://www.linkedin.com/in/saeedmaghdam/

[nuget-page]:https://www.nuget.org/packages/DynaLock

[github]: https://github.com/saeedmaghdam/

[github-page]: https://github.com/saeedmaghdam/DynaLock/
[github-license]: https://raw.githubusercontent.com/saeedmaghdam/DynaLock/master/LICENSE
[CHANGELOG.md]: https://github.com/saeedmaghdam/DynaLock/blob/master/CHANGELOG.md
[DynaLock.Test]: https://github.com/saeedmaghdam/DynaLock/tree/master/DynaLock.Test