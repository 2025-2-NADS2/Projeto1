// src/components/Overlay.jsx
import React, { useEffect, useRef } from "react";
import asteriscoImg from "../assets/overlay/asterisco.png";
import bitmapImg from "../assets/overlay/bitmap.png";
import "../style/overlay.css";

export default function Overlay({ className = "", children }) {
  const containerRef = useRef(null);

  useEffect(() => {
    const container = containerRef.current;
    if (!container) return;

    function distancia(x1, y1, x2, y2) {
      return Math.hypot(x2 - x1, y2 - y1);
    }

    function criarAsterisco() {
      const containerWidth = container.clientWidth;
      const containerHeight = container.clientHeight;
      const posicoes = [];
      const quantidade = Math.min(2, Math.floor(containerWidth / 100) || 1);

      for (let i = 0; i < quantidade; i++) {
        const a = document.createElement("img");
        a.src = asteriscoImg;
        a.classList.add("asterisco-img");

        const size = 30 + Math.random() * 170;
        a.style.width = size + "px";
        a.style.height = size + "px";
        a.style.position = "absolute";

        let x, y, safe = false;
        let tentativas = 0;

        while (!safe && tentativas < 50) {
          x = Math.random() * (containerWidth - size);
          y = Math.random() * (containerHeight - size);
          safe = true;
          for (const p of posicoes) {
            if (distancia(x, y, p.x, p.y) < size) {
              safe = false;
              break;
            }
          }
          tentativas++;
        }
        posicoes.push({ x, y });

        a.style.left = x + "px";
        a.style.top = y + "px";

        const rot = Math.random() * 360;
        a.style.transform = `rotate(${rot}deg)`;

        container.appendChild(a);

        setTimeout(() => a.remove(), 10000);
      }
    }

    const loop = setInterval(() => {
      const existentes = container.querySelectorAll(".asterisco-img");
      if (existentes.length === 0) {
        criarAsterisco();
      }
    }, 1000);

    return () => clearInterval(loop);
  }, []);

  return (
    <div ref={containerRef} className={`overlay-container ${className}`}>
      <div className="bitmap-overlay"></div>
      {children}
    </div>
  );
}
