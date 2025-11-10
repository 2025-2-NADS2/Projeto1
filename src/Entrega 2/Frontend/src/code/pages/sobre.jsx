import React, { useEffect, useRef } from "react";
import "../../style/sobre.css";
import Overlay from "../components/overlay.jsx";

import card_1 from "../../assets/sobre/sobre_card_1.png";
import card_2 from "../../assets/sobre/sobre_card_2.png";
import card_3 from "../../assets/sobre/sobre_card_3.png";

// 🖼️ imagens para a seção de impacto
import impacto_1 from "../../assets/sobre/sobre_info_1.png";
import impacto_2 from "../../assets/sobre/sobre_info_2.png";
import impacto_3 from "../../assets/sobre/sobre_info_3.png";
import impacto_4 from "../../assets/sobre/sobre_info_4.png";

const PaginaMista = () => {
  const numberRefs = useRef([]);

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          const el = entry.target;
          const finalValue = parseInt(el.dataset.value);
          if (entry.isIntersecting) {
            let current = 0;
            const duration = 10; 
            const stepTime = Math.max(10, duration / finalValue);
            const step = Math.ceil(finalValue / 80); // sobe em 80 passos no total
            const interval = setInterval(() => {
              current += step;
              if (current >= finalValue) {
                current = finalValue;
                clearInterval(interval);
              }
              el.textContent = current.toLocaleString("pt-BR");
            }, 15);
          } else {
            // reseta pra 0 quando sai da tela
            el.textContent = "0";
          }
        });
      },
      { threshold: 0.5 }
    );

    numberRefs.current.forEach((ref) => {
      if (ref) observer.observe(ref);
    });

    return () => observer.disconnect();
  }, []);

  return (
    <div className="institucional-page-mista">
      {/* 2. Hero Section / Banner Principal */}
      <Overlay className="hero-section-mista">
        <div className="hero-content-mista">
          <div className="hero-image-mista"></div>
          <div className="hero-text-mista">
            <h1 className="hero-subtitle-mista">Conheça Quem</h1>
            <h2 className="hero-title-mista">Faz Acontecer</h2>
            <p>
              Desenvolvemos projetos sociais para o desenvolvimento de comunidades,
              oferecendo carinho, acolhimento e dignidade.
            </p>
            <div className="hero-actions-mista">
              <button className="btn btn-primary-mista">Ver Nossos Projetos</button>
              <button className="btn btn-secondary-mista">Faça Parte</button>
            </div>
          </div>
        </div>
      </Overlay>

      {/* 3. Seção Sobre Nós */}
      <section className="sobre-nos-mista">
        <h3 className="section-subtitle-mista">Sobre Nós</h3>
        <p className="section-description-mista">
          Comprometidos com a excelência, transformação social e desenvolvimento.
        </p>

        <div className="sobre-nos-cards">
          {[
            {
              title: "Colaboração",
              text: "Acreditamos que o verdadeiro progresso nasce do trabalho em equipe, unindo esforços por um propósito comum.",
              img: card_1,
            },
            {
              title: "Sustentabilidade",
              text: "A sustentabilidade é mais que um princípio — é um compromisso diário entre o ser humano e o meio ambiente.",
              img: card_2,
            },
            {
              title: "Compromisso",
              text: "Trabalhamos com propósito e seriedade. Nosso compromisso é com as pessoas, o futuro e os valores que nos guiam.",
              img: card_3,
            },
          ].map((card, i) => (
            <div key={i} className="card-mista">
              <img src={card.img} alt={card.title} className="card-image-mista" />
              <div className="card-overlay-mista">
                <h4>{card.title}</h4>
                <p>{card.text}</p>
              </div>
            </div>
          ))}
        </div>
      </section>

      {/* 4. Divisor Curvo */}
      <div className="curved-divider top-curved-divider"></div>

      {/* 5. Seção Nossos Valores */}
      <section className="valores-section-mista">
        <div className="valores-content">
          <h3 className="section-title-mista">Nossos Valores</h3>
          <p className="section-description-mista">Princípios que guiam nossas ações.</p>

          <div className="valor-item-mista">
            <span className="valor-icon-mista">👤</span>
            <div className="valor-text">
              <h4>Sociedade</h4>
              <p>
                Acreditamos que o desenvolvimento social nasce da união e da empatia.
                Nos comprometemos a apoiar comunidades e promover inclusão com dignidade e propósito.
              </p>
            </div>
          </div>

          <div className="valor-item-mista">
            <span className="valor-icon-mista">🌸</span>
            <div className="valor-text">
              <h4>Meio ambiente</h4>
              <p>
                Agimos com responsabilidade para preservar o planeta que compartilhamos.
                Buscamos soluções sustentáveis, valorizando o equilíbrio entre progresso e natureza.
              </p>
            </div>
          </div>

          <div className="valor-item-mista">
            <span className="valor-icon-mista">🤝</span>
            <div className="valor-text">
              <h4>Pessoas</h4>
              <p>
                Nosso trabalho é pautado pelo respeito, ética e escuta.
                Cuidamos de cada relação com transparência e valorizamos o potencial humano.
              </p>
            </div>
          </div>
        </div>
      </section>

      {/* 6. Divisor Curvo */}
      <div className="curved-divider bottom-curved-divider"></div>

      {/* 7. Seção Nosso Impacto */}
      <section className="impacto-section-mista">
        <h2 className="section-title-mista">NOSSO IMPACTO</h2>
        <div className="impacto-grid-mista">
          {[
            { num: 2000, label: "Refeições por Semana", img: impacto_1 },
            { num: 200, label: "Cestas Por Mês", img: impacto_2 },
            { num: 5, label: "Projetos Sociais", img: impacto_3 },
            { num: 2500, label: "Presentes Distribuídos No Natal", img: impacto_4 },
          ].map((item, i) => (
            <div key={i} className="impacto-card-mista">
              <img src={item.img} alt={item.label} className="impacto-img-mista" />
              <p
                ref={(el) => (numberRefs.current[i] = el)}
                data-value={item.num}
                className="impacto-number-mista"
              >
                0
              </p>
              <p className="impacto-label-mista">{item.label}</p>
            </div>
          ))}
        </div>
      </section>
    </div>
  );
};

export default PaginaMista;
