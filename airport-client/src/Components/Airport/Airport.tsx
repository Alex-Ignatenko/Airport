import React from "react";
import { useState, useEffect } from "react";
import IFlight from "../../Interfaces/IFlight";
import IStation from "../../Interfaces/IStation";
import * as signalR from "@microsoft/signalr";
import UpdateTable from "../UpdateTable/UpdateTable";
import Station from "../Station/Station";
import "./Airport.css";
import VideoPreview from "../VideoPreview/VideoPreview";
import axios from "axios";


const Airport = () => {
  const [routeData, setrouteData] = useState<IStation[] | null>(null);
  const [flightListData, setFlightListDate] = useState<IFlight[] | null>(null);
  const [isStarted, setIsStarted] = useState(false);

  const handleClick = () => {
    fetch("https://localhost:7263/api/airport/start");
    setIsStarted(true);
  };

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7263/airporthub/")
      .build();

    const handlerouteData = async (routeData: IStation[]) => {
      setrouteData(routeData);
      console.log(routeData);
    };

    const getFlightListData = async (flightListData: IFlight[]) => {
      setFlightListDate(flightListData);
      console.log(flightListData);
    };

    axios.get('https://localhost:7263/api/airport/status').then(response => {
         setIsStarted(response.data)});

    connection.start().then(() => {
      console.log("SignalR connection established.");
      connection.on("GetStations", handlerouteData);
      connection.on("GetFlights", getFlightListData);
    });
  }, []);
  return (
    <>
      <div>
        <div className="main-container">
          <div className="header-container">
            <div className="video-container">
                <VideoPreview/>
            </div>
          </div>
          <div className="mid-container">
            <div className="stations-container">
              <div className="station-row-1-container">
                <div className="station-4-9-container">
                  <Station
                    name="Station 9"
                    stations={routeData}
                    stationIndex={9}
                    cssStationClass="station-mid basic-station"
                  />
                  <Station
                    name="Station 4"
                    stations={routeData}
                    stationIndex={4}
                    cssStationClass="station-mid basic-station"
                  />
                </div>
                <div className="station-3-2-1-container">
                  <Station
                    name="Station 3"
                    stations={routeData}
                    stationIndex={3}
                    cssStationClass="station-sm basic-station"
                  />
                  <Station
                    name="Station 2"
                    stations={routeData}
                    stationIndex={2}
                    cssStationClass="station-sm basic-station"
                  />
                  <Station
                    name="Station 1"
                    stations={routeData}
                    stationIndex={1}
                    cssStationClass="station-sm basic-station"
                  />
                </div>
              </div>
              <div className="station-row-2-container">
                <div className="station-5-8-container">
                  <Station
                    name="Station 5"
                    stations={routeData}
                    stationIndex={5}
                    cssStationClass="station-bg basic-station"
                  />
                  <Station
                    name="Station 8"
                    stations={routeData}
                    stationIndex={8}
                    cssStationClass="station-bg basic-station"
                  />
                </div>
              </div>
              <div className="station-row-3-container">
                <div className="station-6-7-container">
                  <Station
                    name="Station 6"
                    stations={routeData}
                    stationIndex={6}
                    cssStationClass="station-bg basic-station"
                  />
                  <Station
                    name="Station 7"
                    stations={routeData}
                    stationIndex={7}
                    cssStationClass="station-bg basic-station"
                  />
                </div>
              </div>
            </div>
            <UpdateTable flightListData={flightListData} />
          </div>
          <div className="btn-container">
             {isStarted ?  <h3 className="sim-active-title-alt">Simulator Running</h3>: <button onClick={handleClick}>Start</button>} 
          </div>
        </div>
      </div>
    </>
  );
};

export default Airport;
