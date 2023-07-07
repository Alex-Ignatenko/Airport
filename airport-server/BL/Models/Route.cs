public class Route : IRoute
{
	public List<Station> Stations { get; } = new();
	public List<(Station from, Station to)> Edges { get; } = new();
	public Route()
	{
		InitRoute();
	}


    //Inits the route Data struct
    //The route is essentially a Graph DS stations are nodes and thier connections are the edges
    //Movement is done via edges
    //On each move attempt the flight will get the proper edge to move through
    private void InitRoute()
	{

        //Inits all possible stations in airport
        //THis includes two dummy stations the planes start on from eiter route type
        //The dummies are used to streamline the movment from station to station so entery one from both route types can be treated the same
        //The dummy does not exist as far as the end user is concerened 
        Station station0 = new Station("Dummy Station 1");
		Station station1 = new Station("Station 1");
		Station station2 = new Station("Station 2");
		Station station3 = new Station("Station 3");
		Station station4 = new Station("Station 4");
		Station station5 = new Station("Station 5");
		Station station6 = new Station("Station 6");
		Station station7 = new Station("Station 7");
		Station station8 = new Station("Station 8");
		Station station9 = new Station("Station 9");
		Station station10 = new Station("Dummy Station 2");

        //Add all the stations to the route DS  - the nodes of the graph
        this.AddStation(station0);
		this.AddStation(station1);
		this.AddStation(station2);
		this.AddStation(station3);
		this.AddStation(station4);
		this.AddStation(station5);
		this.AddStation(station6);
		this.AddStation(station7);
		this.AddStation(station8);
		this.AddStation(station9);
		this.AddStation(station10);

        //Add all the possible connections between stations to the route DS  - the edges of the graph
        this.AddEdge(station0, station6);   //0
		this.AddEdge(station0, station7);   //1
		this.AddEdge(station1, station2);   //2
		this.AddEdge(station2, station3);   //3
		this.AddEdge(station3, station4);   //4
		this.AddEdge(station4, station5);   //5
		this.AddEdge(station4, station9);   //6
		this.AddEdge(station5, station6);   //7
		this.AddEdge(station5, station7);   //8
		this.AddEdge(station6, station8);   //9
		this.AddEdge(station7, station8);   //10 
		this.AddEdge(station8, station4);   //11
		this.AddEdge(station10 , station1); //12
    }

    //If the DS lacks the current station it will be added
    private void AddStation(Station station)
	{
		if (!Stations.Contains(station))
		{
			Stations.Add(station);
		}
	}

    //If the DS lacks the current edge it will be added
    private void AddEdge(Station from, Station to)
	{
		Edges.Add(new(from, to));
	}


    //The route arriving flights will use
    //the flight class uses this list to know where to move current arriving flight next
    public List<(Station from, Station to)> GetArrivingRoute()
	{
		return new List<(Station from, Station to)>
		{
			this.Edges[12],  //Dummy Station 2 >> Station 1
			this.Edges[2],   //Station 1 >> Station 2
			this.Edges[3],   //Station 2 >> Station 3
			this.Edges[4],   //Station 3 >> Station 4  
			this.Edges[5],   //Station 4 >> Station 5 
			this.Edges[7],   //Station 5 >> Station 6
			this.Edges[8],   //Station 5 >> Station 7  
		};
	}

    //The route departing flights will use
    //the flight class uses this list to know where to move current departing flight next 
    public List<(Station from, Station to)> GetDepartingRoute()
	{
		return new List<(Station from, Station to)>
		{
			this.Edges[0],  //Dummy Station 1 >> Station 6
			this.Edges[1],  //Dummy Station 1 >> Station 7
			this.Edges[9],  //Station 6 >> Station 8
			this.Edges[10], //Station 7 >> Station 8
			this.Edges[11], //Station 8 >> Station 4
			this.Edges[6]	//Station 4 >> Station 9
		};
	}
}



