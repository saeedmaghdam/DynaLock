



# DynaLock

> Create and manage locks dynamically in run-time in concurrent software

  

DynaLock is a library to create and manage locks dynamically in run-time in concurrent software, DynaLock hides all concurrency complexities in distributed systems.
DynaLock currently supports Monitor and Semaphore.

## Important
#### DynaLock is currecntly under development and only supports Monitor
#### In a very near future, Semaphore will be added to DynaLock
#### Please contribute in development!

## Installation

  

Package Manager Console - Visual Studio:

  

```sh

Install-Package DynaLock -Version 1.0.3

```

  

Or visit DynaLock's [Nuget][nuget-page] page to get more information.

  

## Usage example

  

DynaLock is easy to use.

  

```cs

using (var monitor = DynaLock.Monitor("enter_a_unique_name")){

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

using (var monitor = new DynaLock.Monitor(domain1_context, "enter_a_unique_name_in_current_domain")){

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




using (var monitor = new DynaLock.Monitor(domain2_context, "enter_a_unique_name_in_current_domain")){

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