import React from 'react';

import "../../style/admin.css";

const AdminPanel = () => {
  return (
    <div className="admin-body">
      <header className="admin-header">
        <h1>Painel de Administração | Instituto Alma</h1>
      </header>
      
      <div className="admin-container">
        
        {/* ====================================
            1. Gestão de Eventos
        ==================================== */}
        <section className="admin-section eventos-section">
          <h2 className="section-title">📅 Gestão de Eventos (Agenda)</h2>
          <p className="section-description">
            Crie, edite ou remova eventos que aparecerão na agenda do site.
          </p>
          
          <div className="admin-form-container">
            <h3>Criar Novo Evento</h3>
            <form className="admin-form">
              <div className="form-group">
                <label htmlFor="evento-titulo">Título do Evento:</label>
                <input type="text" id="evento-titulo" placeholder="Ex: Mutirão de Doação de Roupas" required />
              </div>
              <div className="form-group grid-2">
                <div className="form-sub-group">
                  <label htmlFor="evento-data">Data:</label>
                  <input type="date" id="evento-data" required />
                </div>
                <div className="form-sub-group">
                  <label htmlFor="evento-hora">Hora:</label>
                  <input type="time" id="evento-hora" />
                </div>
              </div>
              <div className="form-group">
                <label htmlFor="evento-local">Local / Endereço:</label>
                <input type="text" id="evento-local" placeholder="Ex: Sede do Instituto Alma, Rua Exemplo, 123" />
              </div>
              <div className="form-group">
                <label htmlFor="evento-descricao">Descrição Detalhada:</label>
                <textarea id="evento-descricao" rows="4" placeholder="Detalhes, requisitos e como participar." required></textarea>
              </div>
              <div className="form-group">
                <label htmlFor="evento-imagem">Imagem de Destaque:</label>
                <input type="file" id="evento-imagem" accept="image/*" />
              </div>
              <button type="submit" className="btn-admin btn-primary-admin">Salvar Evento</button>
            </form>
            
            <h3 className="list-title">Eventos Atuais (Placeholder)</h3>
            <ul className="item-list">
                <li>
                    <span>Mutirão de Natal - 25/12/2025</span>
                    <div className="item-actions">
                        <button className="btn-admin btn-edit">Editar</button>
                        <button className="btn-admin btn-delete">Excluir</button>
                    </div>
                </li>
            </ul>
          </div>
        </section>

        {/* ====================================
            2. Gestão de Transparência (Relatórios)
        ==================================== */}
        <section className="admin-section transparencia-section">
          <h2 className="section-title">📄 Gestão de Transparência (Relatórios)</h2>
          <p className="section-description">
            Adicione novos relatórios anuais/semestrais para a seção de Transparência (gavetas/acordeão).
          </p>
          
          <div className="admin-form-container">
            <h3>Criar Novo Relatório</h3>
            <form className="admin-form">
              <div className="form-group">
                <label htmlFor="relatorio-titulo">Título da Gaveta:</label>
                <input type="text" id="relatorio-titulo" placeholder="Ex: Relatório Anual 2025" required />
              </div>
              <div className="form-group grid-2">
                <div className="form-sub-group">
                    <label htmlFor="relatorio-ano">Ano/Período:</label>
                    <input type="number" id="relatorio-ano" placeholder="Ex: 2025" min="2000" max="2100" required />
                </div>
                <div className="form-sub-group">
                    <label htmlFor="relatorio-cor">Cor Tema (Gaveta):</label>
                    <select id="relatorio-cor">
                        <option value="laranja">Laranja</option>
                        <option value="azul">Azul Escuro</option>
                        <option value="verde-escuro">Verde Escuro</option>
                    </select>
                </div>
              </div>
              <div className="form-group">
                <label htmlFor="relatorio-descricao">Descrição Curta (Conteúdo):</label>
                <textarea id="relatorio-descricao" rows="3" placeholder="Texto que aparecerá ao abrir a gaveta." required></textarea>
              </div>
              <div className="form-group">
                <label htmlFor="relatorio-pdf">Arquivo PDF do Relatório:</label>
                <input type="file" id="relatorio-pdf" accept=".pdf" required />
              </div>
              <div className="form-group">
                <label htmlFor="relatorio-imagem">Imagem de Destaque (Conteúdo):</label>
                <input type="file" id="relatorio-imagem" accept="image/*" />
              </div>
              <button type="submit" className="btn-admin btn-primary-admin">Salvar Relatório</button>
            </form>
            
            <h3 className="list-title">Relatórios Atuais (Placeholder)</h3>
            <ul className="item-list">
                <li>
                    <span>Relatório Anual 2025 (PDF)</span>
                    <div className="item-actions">
                        <button className="btn-admin btn-edit">Editar</button>
                        <button className="btn-admin btn-delete">Excluir</button>
                    </div>
                </li>
            </ul>
          </div>
        </section>

        {/* ====================================
            3. Gestão de Atividades/Conteúdo
        ==================================== */}
        <section className="admin-section atividades-section">
          <h2 className="section-title">🌟 Gestão de Atividades/Destaques</h2>
          <p className="section-description">
            Gerencie o conteúdo das áreas de Destaques, Quem Faz e outras atividades fixas do site.
          </p>
          
          <div className="admin-form-container">
            <h3>Adicionar Destaque Principal</h3>
            <form className="admin-form">
              <div className="form-group">
                <label htmlFor="atividade-titulo">Título do Destaque:</label>
                <input type="text" id="atividade-titulo" placeholder="Ex: 400 Famílias Impactadas" required />
              </div>
              <div className="form-group">
                <label htmlFor="atividade-link">Link de Mais Detalhes (Opcional):</label>
                <input type="url" id="atividade-link" placeholder="Ex: /sobre-nossas-metricas" />
              </div>
              <button type="submit" className="btn-admin btn-primary-admin">Salvar Destaque</button>
            </form>
            
            <h3 className="list-title">Destaques Atuais (Placeholder)</h3>
            <ul className="item-list">
                <li>
                    <span>400 Famílias Impactadas</span>
                    <div className="item-actions">
                        <button className="btn-admin btn-edit">Editar</button>
                        <button className="btn-admin btn-delete">Excluir</button>
                    </div>
                </li>
            </ul>
          </div>
        </section>
        
      </div>
      
      <footer className="admin-footer">
        <p>&copy; 2025 Instituto Alma | Painel Administrativo</p>
      </footer>
    </div>
  );
};

export default AdminPanel;