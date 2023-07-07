import React from "react";
import IFlight from "../../Interfaces/IFlight";
import './FlightIcon.css';


interface IFlightIconProps {
  cssIndex : number;
  isArriving : boolean;
}

const FlightIcon = ({ cssIndex , isArriving } : IFlightIconProps) => {
  let cssClasses : string = '';

  if (cssIndex === 1 || cssIndex === 2 || cssIndex === 3 || cssIndex === 4 || cssIndex === 9){
    if (isArriving){
      cssClasses = 'left arriving';
    }
    else {
      cssClasses = 'left departing';
    }
  }
  if (cssIndex === 5){
    cssClasses = 'down arriving';
  }
  if (cssIndex === 8){
    cssClasses = 'up departing';
  }
  if (cssIndex === 6 || cssIndex === 7){
    if (isArriving){
      cssClasses = 'down arriving';
    }
    else {
      cssClasses = 'up departing';
    }
  }

  return (
    <>
      <div className={cssClasses}>
        <span className="material-symbols-outlined">flight</span>
      </div>
    </>
  );
};

export default FlightIcon;
