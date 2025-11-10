import React from "react";

// importe suas imagens aqui
import patrocinador1 from "../../assets/patrocinadores/patrocinador_1.png";
import patrocinador2 from "../../assets/patrocinadores/patrocinador_2.png";
import patrocinador3 from "../../assets/patrocinadores/patrocinador_3.png";
import patrocinador4 from "../../assets/patrocinadores/patrocinador_4.png";
import patrocinador5 from "../../assets/patrocinadores/patrocinador_5.png";
import patrocinador6 from "../../assets/patrocinadores/patrocinador_6.png";

import Overlay from './overlay.jsx';

export default function Patrocinadores() {
  const imagens = [patrocinador1, patrocinador2, patrocinador3, patrocinador4, patrocinador5, patrocinador6];

  const lista = imagens.length === 1
    ? Array(8).fill(imagens[0])
    : [...imagens, ...imagens, ...imagens, ...imagens, ...imagens, ...imagens, ...imagens, ...imagens]; 

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
