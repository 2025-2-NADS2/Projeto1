import React, { useState } from "react";
import "../../style/voluntariado.css";

import Overlay from "../components/overlay.jsx";

export default function Volunteer() {
  const [form, setForm] = useState({
    nome: "",
    email: "",
    telefone: "",
    mensagem: "",
  });

  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

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
      {/* HERO */}
      <Overlay className="hero-volunteer">
        <div className="hero-content-volunteer">
          <h1>TRANSFORME VIDAS ATRAVÉS DO VOLUNTARIADO</h1>
          <p>
            junte-se ao instituto alma e faça parte de um time dedicado a ajudar
            pessoas em situação de vulnerabilidade. seu tempo e talento podem
            fazer a diferença.
          </p>
        </div>
      </Overlay>

      {/* FORM */}
      <section className="form-section">
        <h2>candidate-se agora</h2>
        <p className="subtitle">
          preencha o formulário abaixo para entrarmos em contato com você
        </p>

        <form className="volunteer-form" onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label>nome completo *</label>
              <input
                type="text"
                name="nome"
                value={form.nome}
                onChange={handleChange}
                placeholder="seu nome completo"
              />
              {errors.nome && <span className="error">{errors.nome}</span>}
            </div>

            <div className="form-group">
              <label>email *</label>
              <input
                type="email"
                name="email"
                value={form.email}
                onChange={handleChange}
                placeholder="seu email completo aqui"
              />
              {errors.email && <span className="error">{errors.email}</span>}
            </div>
          </div>

          <div className="form-row">
            <div className="form-group half">
              <label>telefone *</label>
              <input
                type="text"
                name="telefone"
                value={form.telefone}
                onChange={handleChange}
                placeholder="seu telefone completo aqui"
              />
              {errors.telefone && (
                <span className="error">{errors.telefone}</span>
              )}
            </div>
          </div>

          <div className="form-group">
            <label>mensagem</label>
            <textarea
              name="mensagem"
              value={form.mensagem}
              onChange={handleChange}
              placeholder="nos conte um pouco sobre você"
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
