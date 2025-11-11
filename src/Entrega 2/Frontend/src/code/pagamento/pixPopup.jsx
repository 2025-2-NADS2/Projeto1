// src/code/components/pixPopup.jsx
import React from "react";
import "../../style/pix.css";

export default function PixPopup({ pixData, onClose }) {
  if (!pixData) return null;

  return (
    <div className="pix-popup-overlay" onClick={onClose}>
      <div className="pix-popup-container" onClick={(e) => e.stopPropagation()}>
        <button className="pix-popup-fechar" onClick={onClose}>×</button>

        <h2 className="pix-popup-titulo">Pagamento via Pix</h2>

        <div className="pix-popup-conteudo">
          <img
            src={pixData.qrCodeUrl}
            alt="QR Code Pix"
            className="pix-popup-qrcode"
          />

          <p className="pix-popup-valor">Valor: R$ {pixData.valor}</p>

          <div className="pix-popup-codigo">
            <textarea
              readOnly
              value={pixData.payload}
              className="pix-codigo-textarea"
            />
            <button
              className="pix-botao-copiar"
              onClick={() => {
                navigator.clipboard.writeText(pixData.payload);
                alert("Código Pix copiado!");
              }}
            >
              Copiar código
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
