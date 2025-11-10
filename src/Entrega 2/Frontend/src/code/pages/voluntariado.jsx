import React, { useState } from "react";
import Overlay from "../components/overlay.jsx";
import "../../style/voluntariado.css";

export default function Volunteer() {
  // Estado do formulário
  const [form, setForm] = useState({
    nome: "",
    email: "",
    telefone: "",
    mensagem: "",
  });

  // Estado de erros de validação
  const [errors, setErrors] = useState({});

  // Atualiza os campos do formulário
  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  // Validação e envio do formulário
  const handleSubmit = (e) => {
    e.preventDefault();

    let newErrors = {};
    if (!form.nome.trim()) newErrors.nome = "Preencha o nome completo.";
    if (!form.email.trim()) newErrors.email = "Preencha o e-mail.";
    if (!form.telefone.trim()) newErrors.telefone = "Preencha o telefone.";
    if (!form.mensagem.trim()) newErrors.mensagem = "Escreva uma mensagem.";

    setErrors(newErrors);

    if (Object.keys(newErrors).length === 0) {
      alert("Candidatura enviada com sucesso!");
      setForm({ nome: "", email: "", telefone: "", mensagem: "" });
    }
  };

  return (
    <div className="volunteer-page">
      {/* Hero / Banner principal */}
      <Overlay className="hero-volunteer">
        <div className="hero-content-volunteer">
          <h1>TRANSFORME VIDAS ATRAVÉS DO VOLUNTARIADO</h1>
          <p>
            Junte-se ao Instituto Alma e faça parte de um time dedicado a ajudar
            pessoas em situação de vulnerabilidade. Seu tempo e talento podem
            fazer a diferença.
          </p>
        </div>
      </Overlay>

      {/* Formulário de candidatura */}
      <section className="form-section">
        <h2>Candidate-se agora</h2>
        <p className="subtitle">
          Preencha o formulário abaixo para entrarmos em contato com você
        </p>

        <form className="volunteer-form" onSubmit={handleSubmit}>
          {/* Linha 1: nome e email */}
          <div className="form-row">
            <div className="form-group">
              <label>Nome completo *</label>
              <input
                type="text"
                name="nome"
                value={form.nome}
                onChange={handleChange}
                placeholder="Seu nome completo"
              />
              {errors.nome && <span className="error">{errors.nome}</span>}
            </div>

            <div className="form-group">
              <label>Email *</label>
              <input
                type="email"
                name="email"
                value={form.email}
                onChange={handleChange}
                placeholder="Seu email completo"
              />
              {errors.email && <span className="error">{errors.email}</span>}
            </div>
          </div>

          {/* Linha 2: telefone */}
          <div className="form-row">
            <div className="form-group half">
              <label>Telefone *</label>
              <input
                type="text"
                name="telefone"
                value={form.telefone}
                onChange={handleChange}
                placeholder="Seu telefone completo"
              />
              {errors.telefone && (
                <span className="error">{errors.telefone}</span>
              )}
            </div>
          </div>

          {/* Mensagem */}
          <div className="form-group">
            <label>Mensagem</label>
            <textarea
              name="mensagem"
              value={form.mensagem}
              onChange={handleChange}
              placeholder="Nos conte um pouco sobre você"
            />
            {errors.mensagem && (
              <span className="error">{errors.mensagem}</span>
            )}
          </div>

          <button type="submit" className="btn-enviar">
            Enviar candidatura ↑
          </button>
        </form>
      </section>
    </div>
  );
}
