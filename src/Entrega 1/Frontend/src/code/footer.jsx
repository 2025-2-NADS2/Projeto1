import React from "react";
import footerImage from "../assets/Footer.svg";
import instagramIcon from "../assets/footer/instagram.svg";
import facebookIcon from "../assets/footer/facebook.svg";
import whatsappIcon from "../assets/footer/whatsapp.svg";
import mapIcon from "../assets/footer/map.svg";

import "../style/footer.css";

export default function Footer() {
  return (
    <footer className="footer">
      {/* Fundo */}
      <img src={footerImage} alt="Footer Background" className="footer-background" />

      {/* Conteúdo */}
      <div className="footer-content">
        <div className="footer-col">
          <h3>Instituto Alma</h3>
          <p>
            Nosso instituto atua ajudando famílias em situação de vulnerabilidade,
            promovendo educação, saúde e apoio social com muito carinho.
          </p>
          <a href="#" className="footer-link">Saiba mais →</a>
        </div>

        <div className="footer-col">
          <h4>Descubra</h4>
          <ul>
            <li><a href="#">Projetos</a></li>
            <li><a href="#">Eventos</a></li>
            <li><a href="#">Seja Voluntário</a></li>
            <li><a href="#">Doações</a></li>
          </ul>
        </div>

        <div className="footer-col">
          <h4>Sobre</h4>
          <ul>
            <li><a href="#">Equipe</a></li>
            <li><a href="#">Nossa História</a></li>
            <li><a href="#">Parceiros</a></li>
            <li><a href="#">Contato</a></li>
          </ul>
        </div>

        <div className="footer-col">
          <h4>Conecte-se</h4>
          <div className="social-icons">
            <img src={instagramIcon} alt="Instagram" />
            <img src={facebookIcon} alt="Facebook" />
            <img src={whatsappIcon} alt="WhatsApp" />
            <img src={mapIcon} alt="Mapa" />
          </div>
        </div>
      </div>

      <hr className="footer-separator" />

      <div className="footer-partners">
        Patrocinadores: Patrocinador 1 • Patrocinador 2 • Patrocinador 3 • Patrocinador 4
      </div>

      <div className="footer-bottom">
        © 2024 Instituto Alma — Todos os direitos reservados.
      </div>
    </footer>
  );
}