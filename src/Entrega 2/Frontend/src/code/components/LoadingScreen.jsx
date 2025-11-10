import React, { useEffect, useState } from "react";
import "../../style/loading.css"; 

export default function LoadingScreen() {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    const handleLoad = () => {
      // começa fade out
      setVisible(false);
    };

    // escuta o carregamento da página
    window.addEventListener("load", handleLoad);

    return () => {
      window.removeEventListener("load", handleLoad);
    };
  }, []);

  if (!visible) return null;

  return (
    <div id="loading-screen" style={{ opacity: visible ? 1 : 0 }}>
      <div className="dots">
        <span></span>
        <span></span>
        <span></span>
      </div>
    </div>
  );
}
