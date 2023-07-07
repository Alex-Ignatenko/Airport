import React from "react";
import IFlight from "../../Interfaces/IFlight";
import "./UpdateTable.css";

interface IUpdateTableProps {
  flightListData: IFlight[] | null;
}

const UpdateTable = ({ flightListData }: IUpdateTableProps) => {
  return (
    <>
      <div className="table-container">
        <div className="arrival-table tbl">
          <h3>
            <u>Arrivals Updates</u>
          </h3>
          <div>
            <ol>{flightListData?.map(checkArriving)}</ol>
          </div>
        </div>
        <div className="departure-table tbl">
          <h3>
            <u>Departures Updates</u>
          </h3>
          <div>
            <ol>{flightListData?.map(checkDeparturing)}</ol>
          </div>
        </div>
      </div>
    </>
  );
};

function checkArriving(flight: IFlight) {
  if (flight != null) {
    return flight.isArriving ? (
      <li>{flight.flightNumber + "  " + flight.flightName}</li>
    ) : null;
  } else {
    return null;
  }
}

function checkDeparturing(flight: IFlight) {
  if (flight != null) {
    return !flight.isArriving ? (
      <li>{flight.flightNumber + "  " + flight.flightName}</li>
    ) : null;
  } else {
    return null;
  }
}

export default UpdateTable;
