// src/components/patrocinadores.jsx
import React from "react";
import Overlay from './overlay.jsx';

// Imagens dos patrocinadores
import patrocinador1 from "../../assets/patrocinadores/patrocinador_1.png";
import patrocinador2 from "../../assets/patrocinadores/patrocinador_2.png";
import patrocinador3 from "../../assets/patrocinadores/patrocinador_3.png";
import patrocinador4 from "../../assets/patrocinadores/patrocinador_4.png";
import patrocinador5 from "../../assets/patrocinadores/patrocinador_5.png";
import patrocinador6 from "../../assets/patrocinadores/patrocinador_6.png";

export default function Patrocinadores() {
  const imagens = [
    patrocinador1,
    patrocinador2,
    patrocinador3,
    patrocinador4,
    patrocinador5,
    patrocinador6,
  ];

  // Se houver apenas 1 imagem, duplica 8x; caso contrário, repete todas as imagens 8x para o efeito marquee
  const lista = imagens.length === 1
    ? Array(8).fill(imagens[0])
    : Array(8).fill(imagens).flat();

  return (
    <Overlay className="patrocinadores-background">
      <section>
        <div className="patrocinadores-container">
          <div className="patrocinadores-marquee">
            {lista.map((img, i) => (
              <div className="patrocinador" key={i}>
                <img src={img} alt={`Patrocinador ${i + 1}`} />
              </div>
            ))}
          </div>
        </div>
      </section>
    </Overlay>
  );
}
