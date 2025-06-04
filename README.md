# TFL-Application-with-Asp.Net-core
A Transport for London (TfL) application that models and oversees specific areas of the TfL overground and tube network systems.
UML Design for Version 1:

Figure 1: UML diagram of version 1

Version 1 follows the MVC design pattern, which stands for Models, Views, and Controllers.
For models, we have custom implementations of a dictionary, list, priority queue, as well as representations for edges, lines, and stations. Each line possesses a collection of many stations, each connected to another to form edges.
Regarding the relationships between these models, a composition seemed rational, as each line is composed of connected stations, each formed of stations. To enable collections and ensure avoidance of generic libraries, custom implementations of a list and a dictionary were utilised. Additionally, a priority queue was implemented for solving shortest path problems, to store nodes to visit.
As for the views, both a customer and an engineering menu were implemented. The customer view allows the user to search for the shortest path from a start station to a finish station. On the other hand, the engineering menu is more complex, offering options to manage stations where the user can add delays, remove delays, close or open an edge, view stations, or access traffic information.

Finally, to connect views to models, a station graph was implemented. This graph holds collections of lines, stations, and edges that are crucial for network maintenance. This controller is where the shortest path method is implemented using Dijkstra's algorithm. It is also responsible for adding lines, stations, and edges, as well as retrieving them.
