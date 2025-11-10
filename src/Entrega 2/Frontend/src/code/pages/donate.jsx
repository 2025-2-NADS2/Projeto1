import React, { useRef } from "react";
import "../../style/donation.css";
import Overlay from "../components/overlay.jsx";
import icon_1 from "../../assets/donate/icon_1.png";
import icon_2 from "../../assets/donate/icon_2.png";
import icon_3 from "../../assets/donate/icon_3.png";

export default function Donation({ onBack, onDonatePortal }) {
  const cardsRef = useRef(null);

  const handleScrollToCards = () => {
    cardsRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  return (
    <div className="donation-page">
      <Overlay className="hero-section">
        <div className="hero-content">
          <h1 className="titulo-donate">NOS AJUDE EM NOSSA MISSÃO</h1>
          <p className="subtitulo-donate">
            Toda ajuda é muito bem-vinda, indo para as pessoas certas, e que vão utilizar muito bem de sua doação.
          </p>
        </div>
      </Overlay>

      {/* META */}
      <section className="goal-section">
        <h2 className="goal-amount">$150,000</h2>

        <div className="progress-bar" aria-hidden>
          <div className="progress-fill" style={{ width: "18%" }} />
        </div>

        <div className="goal-info">
          <span>
            Já arrecadamos <b>R$25.956</b>
          </span>
          <span>98 pessoas já doaram</span>
        </div>
      </section>

      {/* OPÇÕES DE DOAÇÃO */}
      <div className="donation-options">
        <div className="donation-box">
          <div className="donation-img-slot">❤️</div>
          <h3 className="donation-title">Doação única</h3>
          <p className="donation-desc">
            Faça uma contribuição pontual e ajude uma causa urgente com um único gesto.
          </p>
          <button className="btn-primary_donate" onClick={onDonatePortal}>
            Ajudar agora
          </button>
        </div>

        <div className="donation-box">
          <div className="donation-img-slot">💕</div>
          <h3 className="donation-title">Doação mensal</h3>
          <p className="donation-desc">
            Torne-se um apoiador mensal e contribua continuamente para nossas ações.
          </p>
          <button className="btn-primary_donate" onClick={onDonatePortal}>
            Ajudar agora
          </button>
        </div>

        <div className="donation-box">
          <div className="donation-img-slot">🎁</div>
          <h3 className="donation-title">Ajudar de outra forma</h3>
          <p className="donation-desc">
            Doe seu tempo, compartilhe nossa missão ou contribua de outras maneiras.
          </p>
          <button className="btn-primary_donate" onClick={onDonatePortal}>
            Ajudar agora
          </button>
        </div>
      </div>

      {/* CARDS */}
      <section className="categories" ref={cardsRef}>
        {/* ÁGUA LIMPA */}
        <article className="category-card pink">
          <div className="card-top">
            <div className="icon-circle">
              <img className="icon-image" src={icon_1} alt="Água limpa" />
            </div>
            <h3>Água limpa</h3>
          </div>
          <p className="card-text">
            Levamos acesso à água potável para comunidades que enfrentam escassez. Cada doação ajuda a construir poços e sistemas de filtragem que salvam vidas todos os dias.
          </p>
          <div className="card-footer">
            <a
              href="https://www.who.int/news-room/fact-sheets/detail/drinking-water"
              target="_blank"
              rel="noopener noreferrer"
              className="btn-outline"
            >
              Saiba mais <span className="arrow">→</span>
            </a>
          </div>
        </article>

        {/* COMIDA FRESCA */}
        <article className="category-card green">
          <div className="card-top">
            <div className="icon-circle">
              <img className="icon-image" src={icon_2} alt="Comida fresca" />
            </div>
            <h3>Comida fresca</h3>
          </div>
          <p className="card-text">
            Garantimos refeições saudáveis para famílias em situação de vulnerabilidade. Doações são convertidas em cestas básicas e alimentos frescos distribuídos semanalmente.
          </p>
          <div className="card-footer">
            <a
              href="https://www.nhs.uk/live-well/eat-well/how-to-eat-a-balanced-diet/eating-a-balanced-diet/"
              target="_blank"
              rel="noopener noreferrer"
              className="btn-outline"
            >
              Saiba mais <span className="arrow">→</span>
            </a>
          </div>
        </article>

        {/* AJUDA MÉDICA */}
        <article className="category-card navy">
          <div className="card-top">
            <div className="icon-circle">
              <img className="icon-image" src={icon_3} alt="Ajuda médica" />
            </div>
            <h3>Ajuda médica</h3>
          </div>
          <p className="card-text">
            Oferecemos cuidados de saúde essenciais a quem mais precisa. Consultas, medicamentos e campanhas de vacinação chegam a comunidades carentes com sua contribuição.
          </p>
          <div className="card-footer">
            <a
              href="https://www.who.int/our-work/access-to-medicines-and-health-products"
              target="_blank"
              rel="noopener noreferrer"
              className="btn-outline"
            >
              Saiba mais <span className="arrow">→</span>
            </a>
          </div>
        </article>
      </section>
    </div>
  );
}
