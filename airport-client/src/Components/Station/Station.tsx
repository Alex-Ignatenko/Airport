import React from "react";
import IStation from "../../Interfaces/IStation";
import FlightIcon from "../FlightIcon/FlightIcon";
import "./Station.css";

interface IStationProps {
  stationIndex : number;
  stations : IStation[] | null;
  cssStationClass : string;
  name : string;
}

const Station = ({ stations , stationIndex  , cssStationClass ,name} : IStationProps) => {
  return (
    <>
      <div className={cssStationClass}>
        <h5>{name}</h5>
        <div className={stations && (stations[stationIndex].stationName === "Station 5" || stations[stationIndex].stationName === "Station 6" || stations[stationIndex].stationName === "Station 7" || stations[stationIndex].stationName === "Station 8")  ? "station-bg-inner" : ""}>
            {stations && stations[stationIndex].currentFlight ? <FlightIcon cssIndex={stationIndex} isArriving={stations[stationIndex].currentFlight.isArriving} /> : null}
            {stations && stations[stationIndex].currentFlight?.flightNumber} <br />
            {stations && stations[stationIndex].currentFlight?.flightName}
        </div>
        
      </div>
    </>
  );
};

export default Station;
