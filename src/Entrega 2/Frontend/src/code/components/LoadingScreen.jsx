// LoadingScreen.jsx
import React, { useEffect, useState } from "react";
import "../../style/loading.css"; 

export default function LoadingScreen() {
  const [visible, setVisible] = useState(true);

  // Fecha a tela de loading quando a página termina de carregar
  useEffect(() => {
    const handleLoad = () => {
      setVisible(false); // inicia fade out
    };

    window.addEventListener("load", handleLoad);

    return () => {
      window.removeEventListener("load", handleLoad);
    };
  }, []);

  // Se não estiver visível, não renderiza nada
  if (!visible) return null;

  return (
    <div 
      id="loading-screen" 
      style={{ opacity: visible ? 1 : 0 }}
    >
      <div className="dots">
        {/* Animação de pontos */}
        <span></span>
        <span></span>
        <span></span>
      </div>
    </div>
  );
}
