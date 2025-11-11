import React, { useState, useEffect } from "react";
import "../../style/donatePortal.css";
import Overlay from "../components/overlay.jsx";
import PixPopup from "../pagamento/pixPopup.jsx";
import gerarPix from "../pagamento/pix.jsx"; // 🔹 Importa a função geradora do Pix

// Ícone SVG para o card de doação
const IconCard = (props) => (
  <svg
    xmlns="http://www.w3.org/2000/svg"
    width="28"
    height="28"
    viewBox="0 0 24 24"
    fill="none"
    stroke="currentColor"
    strokeWidth="2"
    strokeLinecap="round"
    strokeLinejoin="round"
    {...props}
  >
    <rect x="2" y="5" width="20" height="14" rx="2" />
    <line x1="2" y1="10" x2="22" y2="10" />
  </svg>
);

// Componente do portal de doação
const DonationPortal = ({ onBack, onLoginClick }) => {
  const [donationValue, setDonationValue] = useState("");
  const [paymentMethod, setPaymentMethod] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [showLoginPopup, setShowLoginPopup] = useState(false);
  const [pixData, setPixData] = useState(null); // Dados do Pix

  useEffect(() => {
    const token = localStorage.getItem("token");
    setIsLoggedIn(!!token);
  }, []);

  const handleDonate = (e) => {
    e.preventDefault();

    // if (!isLoggedIn) {
    //   setShowLoginPopup(true);
    //   return;
    // }

    if (paymentMethod === "pix") {
      if (!donationValue || parseFloat(donationValue) <= 0) {
        alert("Digite um valor válido para gerar o Pix.");
        return;
      }

      const pixGerado = gerarPix(parseFloat(donationValue));

      if (!pixGerado) {
        alert("Erro ao gerar o Pix. Verifique o valor informado.");
        return;
      }

      setPixData(pixGerado);
      return;
    }

    // Outros métodos de pagamento simulados
    alert(
      `✅ Doação de R$${donationValue} via ${paymentMethod.toUpperCase()} realizada com sucesso!`
    );

    setDonationValue("");
    setPaymentMethod("");
  };

  return (
    <div className="donation-container">
      {/* SEÇÃO HERO */}
      <Overlay className="hero-section-doacao">
        <div className="hero-content-doacao">
          <h1>Portal do Doador 💚</h1>
          <p>Realize sua doação de forma rápida, segura e com propósito.</p>
        </div>
      </Overlay>

      {/* FORMULÁRIO */}
      <section className="donation-form-section">
        <div className="donation-card">
          <div className="donation-header">
            <div className="icon-box">
              <IconCard />
            </div>
            <h2>Doação online</h2>
          </div>

          <form onSubmit={handleDonate} className="donation-form">
            <label htmlFor="valor">Valor da Doação (R$)</label>
            <input
              type="number"
              id="valor"
              placeholder="Digite o valor"
              value={donationValue}
              onChange={(e) => setDonationValue(e.target.value)}
              required
            />

            <label htmlFor="metodo">Método de Pagamento</label>
            <select
              id="metodo"
              value={paymentMethod}
              onChange={(e) => setPaymentMethod(e.target.value)}
              required
            >
              <option value="">Selecione</option>
              <option value="pix">Pix</option>
              <option value="stripe">Stripe</option>
              <option value="mercadopago">Mercado Pago</option>
              <option value="pagseguro">PagSeguro</option>
            </select>

            <button type="submit" className="donate-btn">
              Finalizar Doação
            </button>
          </form>

          {/* Botão Voltar */}
          <button className="btn-back" onClick={onBack}>
            ← Voltar
          </button>
        </div>
      </section>

      {/* POPUP DO PIX */}
      {pixData && <PixPopup pixData={pixData} onClose={() => setPixData(null)} />}

      {/* POPUP DE LOGIN */}
      {showLoginPopup && (
        <div className="modal-backdrop">
          <div className="modal-content">
            <button
              className="close-btn"
              onClick={() => setShowLoginPopup(false)}
            >
              ×
            </button>
            <h3 className="modal-title">É necessário estar logado</h3>
            <p className="modal-body">
              Para realizar uma doação, por favor faça login na sua conta.
            </p>
            <button className="modal-login-button" onClick={onLoginClick}>
              Logar agora
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default DonationPortal;
