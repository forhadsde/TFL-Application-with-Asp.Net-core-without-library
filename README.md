# TFL-Application-with-Asp.Net-core
A Transport for London (TfL) application that models and oversees specific areas of the TfL overground and tube network systems.
UML Design for Version 1:

Figure 1: UML diagram of TFL APP

<img width="821" alt="vp1" src="https://github.com/user-attachments/assets/f9620935-51e1-4479-b040-3527cbfd2c12" />


This application follows the MVC design pattern, which stands for Models, Views, and Controllers.
For models, we have custom implementations of a dictionary, list, priority queue, as well as representations for edges, lines, and stations. Each line possesses a collection of many stations, each connected to another to form edges.
Regarding the relationships between these models, a composition seemed rational, as each line is composed of connected stations, each formed of stations. To enable collections and ensure avoidance of generic libraries, custom implementations of a list and a dictionary were utilised. Additionally, a priority queue was implemented for solving shortest path problems, to store nodes to visit.
As for the views, both a customer and an engineering menu were implemented. The customer view allows the user to search for the shortest path from a start station to a finish station. On the other hand, the engineering menu is more complex, offering options to manage stations where the user can add delays, remove delays, close or open an edge, view stations, or access traffic information.

Finally, to connect views to models, a station graph was implemented. This graph holds collections of lines, stations, and edges that are crucial for network maintenance. This controller is where the shortest path method is implemented using Dijkstra's algorithm. It is also responsible for adding lines, stations, and edges, as well as retrieving them.

Data Structure:

We compared two alternatives for the representation of the metro map, the adjacency list and the adjacency matrix, and finally adopted the adjacency list for the following reasons
Characteristics of metro networks: connections between metro stations are limited and have the characteristics of sparse graphs. Therefore, the adjacency list is more suitable when memory efficiency is important.
Algorithm choice: Dijkstra is used as the algorithm for finding the shortest path. The Dijkstra method finds the vertex with the lowest cost at each step and then updates the cost by exploring the edges coming out of that vertex. It is often more efficient to use adjacency lists in this process. This is because the adjacency list is more efficient in terms of both computation time and memory, as it can ignore unconnected edges and only process the necessary edge information.

In addition, we chose a weighted directed graph as the data structure for the graph due to the following characteristics.
In order to assess the time taken between stations in the calculation of the shortest route, it is necessary to give the data between stations a weight of Time.
As the time taken between stations may differ slightly depending on the direction in the data, it is necessary to store the data for each direction.

List, Dictionary and PriorityQueue were also used as more generic data structures to realise the graph structure and the Dijkstra method algorithm.

Classes:
Station, Line, Edge, and StationGraph
The Station, Line, Edge and StationGraph classes realise the Metro map graph concept and play the following roles respectively.
Station: class for storing station information. It is the Role of the Node in the graph structure. Each Station object has a property called Edges, which holds a list of edges from that station to other stations directly connected to it. This manages the connection information from each station to the other stations and acts as an adjacency list.
Edge: a class that stores information between stations, with information on stations at both ends, time taken, status and delay state. It is the Edge of the graph structure. It has a weight called Time to realise a weighted directed graph. It also has delay information and information such as whether the station is closed or not as attributes to meet the requirements of the application.
Line: a class that stores information on lines, with information on line names and directions.
StationGraph: a class that stores information on the entire metro map, including information on all stations, lines, and edges. It has a method to search for the shortest route using the Dijkstra method. 

CustomList, CustomDictionary, and CustomPriorityQueue
The CustomList and CustomDictionary classes play a more fundamental role in realising the four graph structures 'Station, Line, Edge and StationGraph' and CustomPriorityQueue is a generic class required to realise the algorithm for finding the shortest path in a StationGraph.
CustomList: a generic, dynamic array implementation in C#. It starts with an initial capacity of four items and expands by doubling its size when more space is needed. Key functionalities include adding and removing items, checking for item existence, and accessing items by index. Elements can be added to the list using Add(T item), which automatically increases the array size when necessary. Items can be removed either directly by item via Remove(T item) or by index with RemoveAt(int index). The list supports iteration and provides utility methods such as Clear(), Contains(T item), Reverse(), and searching with FirstOrDefault(Func<T, bool> predicate). 
CustomDictionary: a generic dictionary implementation using two CustomList<T> instances: one for keys and one for values. It ensures each key is unique and throws exceptions for null keys or duplicate keys during the Add operation. Lookup operations retrieve values based on key index, and the dictionary allows direct value assignment to existing keys via the indexer. If a key does not exist, the indexer adds the key-value pair. This implementation provides ContainsKey to check for the presence of a key and uses IndexOfKey for finding the index of a key within the keys list. The dictionary maintains a count of key-value pairs that is accessible through the Count property. This custom structure is tailored to manage key-value associations while leveraging the dynamic capabilities of the CustomList<T> for storage and operations.
The CustomPriorityQueue:  a generic implementation of a priority queue where each element is associated with a priority, and these priorities determine the order of element retrieval. The queue is backed by a CustomList<(TElement element, TPriority priority)>, maintaining a heap structure to ensure that the element with the highest priority (or lowest depending on the comparison logic) is always at the front. The Enqueue method adds elements to the heap and ensures the heap property is maintained by bubbling up the newly added element if its priority is higher than its parent's. Conversely, the Dequeue method removes and returns the element with the highest priority, then re-adjusts the heap by moving the last element to the root and letting it sink down to its proper position, maintaining the heap property.

Program Testing:
This version of the TfL application underwent rigorous testing procedures to ensure its functionality, reliability, and adherence to requirements. The testing process encompassed various methodologies, including unit testing, integration testing, and acceptance testing.
 
Unit Testing:
 
Unit tests were conducted to validate the functionality of individual components or units within the application. Each function and method was tested independently to verify its correctness and robustness. Test cases were designed to cover a wide range of scenarios, including normal operation, boundary conditions, and error handling.
  
Integration Testing:
 
Integration testing was performed to assess the interaction and integration between different modules or components of the application. This testing phase focused on verifying the seamless collaboration between various system parts, ensuring that data flows correctly and functionalities are integrated cohesively.
 
Acceptance Testing:
 
Acceptance testing was carried out to evaluate the overall compliance of this applicaiton with the specified requirements and user expectations. Test scenarios were designed to simulate real-world usage scenarios, allowing stakeholders to validate the application's suitability for its intended purpose. Feedback from stakeholders and end-users was incorporated to refine the application further.
 
Testing Cases:
 
Examples of testing cases include:
 
Verifying the accuracy of journey time calculations between different stations.
Testing the addition and removal of delays on track sections between stations.
Validating the opening and closing of track sections in one or both directions.
Ensuring correct printing lists of closed track and delayed journey track sections.

Test Case 1: Shortest Path
Description: Check the shortest path from Marble Arch to Great Portland Street

Test Steps:

Enter start station = Marble Arch

Enter destination station = Great Portland Street

Expected Results:

Ride from Marble Arch (Eastbound) to Bond Str on Central (1.25 min)

Change to Jubilee at Bond Str (2 min)

Ride to Baker Str on Jubilee (2.5 min)

Change to H&C at Baker Str (2 min)

Ride to Great Portland Street on H&C (2 min)

Total Time = 9.75 min

Actual Result: Same as expected

Status: ✅ Pass

Test Case 2: Shortest Path + Delayed Time
Description: Check shortest path from Marble Arch to Great Portland Street with a delay

Test Steps:

Add 3-min delay from Marble Arch to Bond Street

Calculate shortest path again

Expected Results:

Same route as above, but

Total Time = 12.75 min

Actual Result: Same as expected

Status: ✅ Pass

Test Case 3: Shortest Path + Closed Station
Description: Check path when a direct edge is closed (Marble Arch to Bond Street)

Test Steps:

Enter start = Marble Arch

Enter destination = Great Portland Street

Mark Marble Arch → Bond Street as closed

Expected Results:

New route:

Marble Arch → Lancaster Gate (2 min)

Lancaster Gate → Queensway (1.75 min)

Queensway → Notting Hill Gate (1.5 min)

Change to Circle Line → Bayswater (2 min)

Bayswater → Paddington (2.01 min)

Paddington → Edgware Road (3.72 min)

Edgware Road → Baker Street (2.5 min)

Baker Street → Great Portland Street (2 min)

Total = 19.48 min

Actual Result: Same as expected

Status: ✅ Pass

Test Case 4: Open Station
Description: Reopen a previously closed station

Test Steps:

Enter start = Marble Arch

Enter destination = Bond Street

Reopen closed edge

Expected Results:

Station removed from closed list

Marble Arch to Bond Street is now accessible

Actual Result: Confirmed — no closed stations listed

Status: ✅ Pass

Big-O Analysis:

Shortest Path Algorithm: This application utilises Dijkstra's algorithm to find the shortest path between two stations in the metro network. The time complexity of Dijkstra's algorithm is O((V + E) log V), where V represents the number of vertices (stations) and E represents the number of edges (connections between stations). This algorithm efficiently determines the shortest path by exploring neighbouring stations based on their connection times.
Custom Data Structures: CustomList, CustomDictionary, and CustomPriorityQueue are employed to manage station, line, edge, and graph data structures efficiently. These custom data structures mimic the functionality of their counterparts in the System.Collections.Generic namespace, with comparable time complexities for common operations.

Execution Time:
For smaller metro networks with fewer stations and connections, this applciaiton demonstrated competitive performance, with route calculations typically completed within the range of 10 to 50 milliseconds.
However, as the network size increased, the execution time of the application also grew proportionally. For networks with hundreds or thousands of stations, route calculations could take anywhere from 100 to 500 milliseconds or more, depending on the complexity of the network and the distance between stations.


