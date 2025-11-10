import React, { useState } from 'react';
import "../../style/ouvidoria.css";

const InfoIcon = () => (
  <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
    <circle cx="12" cy="12" r="10" />
    <line x1="12" y1="16" x2="12" y2="12" />
    <line x1="12" y1="8" x2="12.01" y2="8" />
  </svg>
);

const CloseIcon = ({ onClick }) => (
  <svg onClick={onClick} xmlns="http://www.w3.org/2000/svg" width="22" height="22" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round" className="close-icon">
    <line x1="18" y1="6" x2="6" y2="18" />
    <line x1="6" y1="6" x2="18" y2="18" />
  </svg>
);

const Ouvidoria = ({ onBack }) => {
  const [formData, setFormData] = useState({
    cpf: '',
    telefone: '',
    cidade: '',
    assunto: '',
    mensagem: '',
    aceitePrivacidade: false,
  });
  const [status, setStatus] = useState('');
  const [loading, setLoading] = useState(false);

  const API_BASE = "http://localhost:5000/api/Ouvidoria";

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({ ...prev, [name]: type === "checkbox" ? checked : value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.assunto || !formData.mensagem) {
      setStatus('⚠️ Por favor, preencha o Assunto e a Mensagem.');
      return;
    }

    if (!formData.aceitePrivacidade) {
      setStatus('⚠️ É necessário aceitar o Aviso de Privacidade.');
      return;
    }

    const token = localStorage.getItem('authToken');
    if (!token) {
      setStatus('⚠️ É necessário estar logado para enviar uma mensagem.');
      return;
    }

    setLoading(true);
    setStatus('Enviando...');

    const payload = {
      CPF: formData.cpf,
      Telefone: formData.telefone,
      Cidade: formData.cidade,
      Assunto: formData.assunto,
      Mensagem: formData.mensagem,
    };

    try {
      const response = await fetch(`${API_BASE}/enviar`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Bearer ${token}`,
        },
        body: JSON.stringify(payload),
      });

      if (response.ok) {
        setStatus('✅ Mensagem enviada com sucesso!');
        setFormData({
          cpf: '',
          telefone: '',
          cidade: '',
          assunto: '',
          mensagem: '',
          aceitePrivacidade: false,
        });
      } else if (response.status === 401) {
        setStatus('❌ Não autorizado. Faça login novamente.');
      } else {
        const errorText = await response.text();
        setStatus(`❌ Erro: ${errorText}`);
      }
    } catch (error) {
      setStatus(`❌ Falha na conexão: ${error.message}`);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="ouvidoria-container">
      <div className="ouvidoria-box">
        <header className="ouvidoria-header">
          <div className="title-group">
            <InfoIcon />
            <h2>Ouvidoria</h2>
          </div>
          <CloseIcon onClick={onBack} />
        </header>

        <p className="ouvidoria-desc">
          Deixe suas <strong>críticas, dúvidas ou sugestões</strong> sobre o nosso trabalho.
        </p>

        <form onSubmit={handleSubmit} className="ouvidoria-form">
          <input
            type="text"
            name="assunto"
            placeholder="Assunto"
            value={formData.assunto}
            onChange={handleChange}
            required
          />
          <input
            type="text"
            name="cpf"
            placeholder="CPF"
            value={formData.cpf}
            onChange={handleChange}
          />
          <input
            type="tel"
            name="telefone"
            placeholder="Telefone"
            value={formData.telefone}
            onChange={handleChange}
          />
          <input
            type="text"
            name="cidade"
            placeholder="Cidade"
            value={formData.cidade}
            onChange={handleChange}
          />
          <textarea
            name="mensagem"
            placeholder="Mensagem..."
            rows="6"
            value={formData.mensagem}
            onChange={handleChange}
            required
          />

          <div className="privacidade">
            <input
              type="checkbox"
              name="aceitePrivacidade"
              id="aceitePrivacidade"
              checked={formData.aceitePrivacidade}
              onChange={handleChange}
              required
            />
            <label htmlFor="aceitePrivacidade">
              Li e concordo com o 
              <a href="/aviso-de-privacidade" target="_blank"> Aviso de Privacidade do Alma</a>.
            </label>
          </div>

          {status && <p className="status">{status}</p>}

          <button type="submit" disabled={loading || !formData.aceitePrivacidade}>
            {loading ? 'ENVIANDO...' : 'ENVIAR'}
          </button>
        </form>
      </div>
    </div>
  );
};

export default Ouvidoria;
