import React from "react";
import introVideo from './AirportLogo.mp4';
import "./VideoPreview.css";

const VideoPreview = () => {
  return (
        <video autoPlay muted>
          <source src={introVideo} type="video/mp4" />
          Your browser does not support the video tag.
        </video>
  );
}


export default VideoPreview;