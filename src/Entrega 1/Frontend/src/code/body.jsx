import "../style/App.css";
import start_format from '../assets/start_format.svg';
import destaques_format from '../assets/destaques_format.svg';
import carossel_left from '../assets/carrossel_left.png';
import carossel_right from '../assets/carrossel_right.png';

import { useState, useEffect } from "react";

import '../style/header.css';
import '../style/body.css';
import '../style/footer.css';

import Header from './header.jsx';
import Footer from './footer.jsx';
import Patrocinadores from './patrocinadores.jsx';
import Overlay from './overlay.jsx';
import LoadingScreen from "./LoadingScreen.jsx";


import img1 from "../assets/carrossel/img1.jpg";
import img2 from "../assets/carrossel/img2.jpg";
import img3 from "../assets/carrossel/img3.jpg";
import img4 from "../assets/carrossel/img4.jpg";

// Hook do carrossel

function useCarrossel(tamanho) {
  const [indice, setIndice] = useState(0);

  const anterior = () => {
    setIndice((prev) => (prev === 0 ? tamanho - 1 : prev - 1));
  };

  const proximo = () => {
    setIndice((prev) => (prev === tamanho - 1 ? 0 : prev + 1));
  };

  return { indice, anterior, proximo, setIndice };
}

function Carrossel({ imagens }) {
  const { indice, anterior, proximo } = useCarrossel(imagens.length);
  const [progresso, setProgresso] = useState(0);

  // controla autoplay + progressbar
  useEffect(() => {
    const intervalo = setInterval(() => {
      setProgresso((prev) => {
        if (prev >= 100) {
          proximo();
          return 0;
        }
        return prev + 1; //10s total
      });
    }, 100);

    return () => clearInterval(intervalo);
  }, []);

  return (

    <div className="carrossel">
      {/* Container só da imagem */}
      <div className="carrossel_conteudo">
        <img
          src={imagens[indice]}
          alt={`Imagem ${indice + 1}`}
          style={{ width: "500px", height: "700px", objectFit: "cover" }}
          className="carrossel_interior"
        />

        {/* Progressbar */}

        
        <div className="progressbar">
          <div
            className="progressbar-fill"
            style={{ width: `${progresso}%` }}
          ></div>
        </div>
      </div>

      {/* Botões */}
      <div className="botoes">
        <button className="botao_carrossel_1" onClick={anterior}>
          <img src={carossel_left} alt="ALMA" />
        </button>
        <button className="botao_carrossel_2" onClick={proximo}>
          <img src={carossel_right} alt="ALMA" />
        </button>
      </div>

      {/* Bolinhas de navegação */}
      <div className="carrossel-indicadores-background">
        <div className="carrossel-indicadores">
          {imagens.map((_, i) => (
            <span
              key={i}
              className={`indicador ${i === indice ? "ativo" : ""}`}
            ></span>
          ))}
        </div>
      </div>
    </div>
  );
}

// App principal
export default function App() {
  const imagens = [img1, img2, img3, img4];

  return (
    <div className="app">

      {Header()}

      {/* Introdução */}
      <Overlay>
        <section className="section-com-fundo">
          <section className="intro-effect" alt="Background Effect"></section>
          <img className="start_format" src={start_format} alt="Background Shape" />
          <div className="conteudo">
            
            {/* Carrossel */}
            <div className="inicio_esquerda">
              <Carrossel imagens={imagens} />
            </div>
            <div className="inicio_direita">
              <div className="intro-right">
                <h2 className="titulo-instituto">
                  <span className="instituto_title_name">Instituto</span>{" "}
                  <span className="alma_title_name">Alma</span>
                  <span className="titulo-underline"></span>
                </h2>
                <p className="titulo-descricao-instituto">
                  O Instituto Alma nasceu com o propósito de transformar 
                  vidas, oferecendo apoio, cuidado e esperança a pessoas em diferentes 
                  situações. Cada ação, cada projeto, cada iniciativa busca acolher, 
                  inspirar e fortalecer aqueles que precisam. Trabalhamos com dedicação, 
                  empatia e comprometimento, valorizando a dignidade de cada indivíduo. 
                  Nosso foco é criar um ambiente seguro e humano, onde todos possam crescer, 
                  superar desafios e sentir que não estão sozinhos. Acreditamos no poder da 
                  solidariedade, da educação e do amor ao próximo. Junte-se a nós, conheça 
                  nossas ações e descubra como juntos podemos fazer a diferença. Saiba mais 
                  sobre nosso trabalho.
                </p>
                <button className="btn-primary">Saiba mais</button>
              </div>
            </div>
          </div>
        </section>
      </Overlay>

      {/* Destaques */}
      <section
        className="destaques"
        style={{ backgroundImage: `url(${destaques_format})` }}
      >
        <h3>Destaques</h3>
        <div className="destaques-grid">
          <div className="slot"></div>
          <div className="slot"></div>
          <div className="slot"></div>
          <div className="slot"></div>
        </div>
        <button className="btn-primary btn-more">mais...</button>
      </section>

      {/* Área Verde Escura */}
      <Patrocinadores />

      {/* Quem Fez */}
      <section className="quem-fez">
        <h4>QUEM FEZ</h4>
        <h3>ACONTECER</h3>
        <div className="quem-fez-grid">
          <div className="perfil-slot">
            <div className="perfil"></div>
            <p>Texto1</p>
          </div>
          <div className="perfil-slot">
            <div className="perfil"></div>
            <p>Texto2</p>
          </div>
                    <div className="perfil-slot">
            <div className="perfil"></div>
            <p>Texto3</p>
          </div>
          <div className="perfil-slot">
            <div className="perfil"></div>
            <p>Texto4</p>
          </div>
        </div>
      </section>

      {Footer()}
    </div>
  );
}
