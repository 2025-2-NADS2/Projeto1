// src/components/Overlay.jsx
import React, { useEffect, useRef } from "react";
import asteriscoImg from "../../assets/overlay/asterisco.png";
import bitmapImg from "../../assets/overlay/bitmap.png";
import "../../style/overlay.css"; 

export default function Overlay({ className = "", children }) {
  const containerRef = useRef(null);

  useEffect(() => {
    const container = containerRef.current;
    if (!container) return;

    // Calcula a distância entre dois pontos
    function distancia(x1, y1, x2, y2) {
      return Math.hypot(x2 - x1, y2 - y1);
    }

    // Cria um asterisco flutuante
    function criarAsterisco() {
      const containerWidth = container.clientWidth;
      const containerHeight = container.clientHeight;
      const posicoes = [];
      const quantidade = Math.min(2, Math.floor(containerWidth / 100) || 1);

      for (let i = 0; i < quantidade; i++) {
        const a = document.createElement("img");
        a.src = asteriscoImg;
        a.classList.add("asterisco-img");

        // Tamanho aleatório
        const size = 30 + Math.random() * 170;
        a.style.width = size + "px";
        a.style.height = size + "px";
        a.style.position = "absolute";

        // Evita sobreposição de asteriscos
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

        // Rotação inicial e duração da animação
        const rot = Math.floor(Math.random() * 360);
        const dur = (8 + Math.random() * 10).toFixed(2) + "s";

        a.style.setProperty("--rot", `${rot}deg`);
        a.style.setProperty("--dur", dur);

        // Opacidade aleatória
        const opa = 0.6 + Math.random() * 0.4;
        a.style.setProperty("--opa", opa.toString());

        container.appendChild(a);

        // Remove o asterisco após 10s
        setTimeout(() => a.remove(), 10000);
      }
    }

    // Loop principal para gerar novos asteriscos
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
      {/* Bitmap estático por trás dos elementos */}
      <div className="bitmap-overlay"></div>
      {children}
    </div>
  );
}
