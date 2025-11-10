import React from "react";
import footerImage from "../../assets/Footer.svg";
import instagramIcon from "../../assets/footer/instagram.svg";
import facebookIcon from "../../assets/footer/facebook.svg";
import whatsappIcon from "../../assets/footer/whatsapp.svg";
import mapIcon from "../../assets/footer/map.svg";
import "../../style/footer.css"; 

export default function Footer({ 
  onSobreClick, 
  onAtividadesClick, 
  onEventosClick, 
  onDonateClick, 
  onTransparenciaClick, 
  onVoluntarieClick 
}) {

  // Chama o callback passado para cada link, se existir
  const handleLinkClick = (callback) => {
    if (callback) callback();
  };

  // Copia o telefone para a área de transferência
  const handleCopyPhone = () => {
    const phone = "11999999999";
    navigator.clipboard.writeText(phone)
      .then(() => alert(`Número copiado: ${phone}`))
      .catch(() => alert("Falha ao copiar o número"));
  };

  return (
    <footer className="footer">
      {/* Imagem de fundo do footer */}
      <img src={footerImage} alt="Footer Background" className="footer-background" />

      {/* Conteúdo principal do footer */}
      <div className="footer-content">

        {/* Coluna 1: Sobre o Instituto */}
        <div className="footer-col">
          <h3>Instituto Alma</h3>
          <p>
            Nosso instituto atua ajudando famílias em situação de vulnerabilidade,
            promovendo educação, saúde e apoio social com muito carinho.
          </p>
          <a 
            href="#" 
            className="footer-col-link" 
            onClick={() => handleLinkClick(onSobreClick)}
          >
            Saiba mais →
          </a>
        </div>

        {/* Coluna 2: Descubra */}
        <div className="footer-col">
          <h4>Descubra</h4>
          <ul>
            <li><a href="#" onClick={() => handleLinkClick(onSobreClick)}>Projetos</a></li>
            <li><a href="#" onClick={() => handleLinkClick(onEventosClick)}>Eventos</a></li>
            <li><a href="#" onClick={() => handleLinkClick(onVoluntarieClick)}>Seja Voluntário</a></li>
            <li><a href="#" onClick={() => handleLinkClick(onDonateClick)}>Doações</a></li>
            <li><a href="#" onClick={() => handleLinkClick(onTransparenciaClick)}>Transparência</a></li>
          </ul>
        </div>

        {/* Coluna 3: Sobre */}
        <div className="footer-col">
          <h4>Sobre</h4>
          <ul>
            <li><a href="#">Equipe</a></li>
            <li><a href="#">Nossa História</a></li>
            <li><a href="#">Parceiros</a></li>
            <li><a href="#">Contato</a></li>
          </ul>
        </div>

        {/* Coluna 4: Redes Sociais */}
        <div className="footer-col">
          <h4>Conecte-se</h4>
          <div className="social-icons">
            <a href="https://www.instagram.com/institutoalma" target="_blank" rel="noreferrer">
              <img src={instagramIcon} alt="Instagram" />
            </a>
            <a href="https://www.facebook.com/institutoalma" target="_blank" rel="noreferrer">
              <img src={facebookIcon} alt="Facebook" />
            </a>
            <button 
              onClick={handleCopyPhone} 
              style={{ background: 'none', border: 'none', cursor: 'pointer', padding: 0 }}
            >
              <img src={whatsappIcon} alt="WhatsApp" />
            </button>
            <a href="https://www.google.com/maps/place/Instituto+Alma" target="_blank" rel="noreferrer">
              <img src={mapIcon} alt="Mapa" />
            </a>
          </div>
        </div>
      </div>

      {/* Separador */}
      <hr className="footer-separator" />

      {/* Seção de patrocinadores */}
      <div className="footer-partners">
        Patrocinadores: Carmensitas • Big Pão • Cacau Show • Azon • Apoio Estratégico • Mocotó
      </div>

      {/* Rodapé inferior */}
      <div className="footer-bottom">
        © 2024 Instituto Alma — Todos os direitos reservados.
      </div>
    </footer>
  );
}
