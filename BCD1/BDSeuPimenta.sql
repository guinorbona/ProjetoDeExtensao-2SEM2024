DROP DATABASE IF EXISTS seupimenta;
CREATE DATABASE seupimenta;
USE seupimenta;

-- Tabela de Endereços
CREATE TABLE endereco (
    id INT AUTO_INCREMENT PRIMARY KEY,
    rua VARCHAR(255) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    bairro VARCHAR(100) NOT NULL,
    cidade VARCHAR(100) NOT NULL,
    estado VARCHAR(50) NOT NULL,
    cep VARCHAR(10) NOT NULL
);

-- Tabela de Telefones
CREATE TABLE telefone (
    id INT AUTO_INCREMENT PRIMARY KEY,
    numero VARCHAR(20) NOT NULL UNIQUE
);

-- Tabela de Categorias
CREATE TABLE categoria (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL UNIQUE
);

-- Tabela de Subcategorias
CREATE TABLE subcategoria (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    id_categoria INT NOT NULL,
    FOREIGN KEY (id_categoria) REFERENCES categoria(id)
);

-- Tabela de Unidade de Medida
CREATE TABLE undmedida (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL UNIQUE
);

-- Tabela de Produtos
CREATE TABLE produto (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    descricao TEXT,
    imagem VARCHAR(255),
    valor_compra DECIMAL(10, 2),
    valor_venda DECIMAL(10, 2),
    quantidade FLOAT,
    id_unidade_medida INT,
    id_categoria INT,
    id_subcategoria INT,
    FOREIGN KEY (id_unidade_medida) REFERENCES undmedida(id),
    FOREIGN KEY (id_categoria) REFERENCES categoria(id),
    FOREIGN KEY (id_subcategoria) REFERENCES subcategoria(id)
);

-- Tabela de Fornecedores
CREATE TABLE fornecedor (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome_fantasia VARCHAR(100) NOT NULL,
    razao_social VARCHAR(100) NOT NULL UNIQUE,
    cnpj VARCHAR(18) NOT NULL UNIQUE,
    id_endereco INT NOT NULL,
    id_telefone INT NOT NULL,
    FOREIGN KEY (id_endereco) REFERENCES endereco(id),
    FOREIGN KEY (id_telefone) REFERENCES telefone(id)
);

-- Tabela de Cargos
CREATE TABLE cargo (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL UNIQUE,
    data_inicio DATE NOT NULL,
    data_fim DATE
);

-- Tabela de Funcionários
CREATE TABLE funcionario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(14) NOT NULL UNIQUE,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(100) NOT NULL,
    id_endereco INT NOT NULL,
    id_telefone INT NOT NULL,
    id_cargo INT NOT NULL,
    FOREIGN KEY (id_endereco) REFERENCES endereco(id),
    FOREIGN KEY (id_telefone) REFERENCES telefone(id),
    FOREIGN KEY (id_cargo) REFERENCES cargo(id)
);

-- Tabela de Salários
CREATE TABLE salario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_funcionario INT NOT NULL,
    salario DECIMAL(10, 2) NOT NULL,
    data_inicio DATE NOT NULL,
    data_fim DATE,
    FOREIGN KEY (id_funcionario) REFERENCES funcionario(id)
);

-- Tabela de Usuários
CREATE TABLE usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    senha VARCHAR(100) NOT NULL,
    id_funcionario INT NOT NULL,
    FOREIGN KEY (id_funcionario) REFERENCES funcionario(id)
);

-- Tabela de Clientes
CREATE TABLE cliente (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf_cnpj VARCHAR(20),
    rg_ie VARCHAR(20),
    razao_social VARCHAR(100),
    tipo VARCHAR(20),
    id_endereco INT,
    id_telefone INT,
    FOREIGN KEY (id_endereco) REFERENCES endereco(id),
    FOREIGN KEY (id_telefone) REFERENCES telefone(id)
);

-- Tabela de Tipos de Pagamento
CREATE TABLE tipopagamento (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(90) NOT NULL UNIQUE
);

-- Tabela de Compras
CREATE TABLE compra (
    id INT AUTO_INCREMENT PRIMARY KEY,
    data DATETIME NOT NULL,
    nfiscal INT,
    total DECIMAL(10, 2),
    parcelas INT,
    status VARCHAR(100),
    id_fornecedor INT,
    id_tipo_pagamento INT,
    FOREIGN KEY (id_fornecedor) REFERENCES fornecedor(id),
    FOREIGN KEY (id_tipo_pagamento) REFERENCES tipopagamento(id)
);

-- Tabela de Itens de Compra
CREATE TABLE itenscompra (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quantidade FLOAT NOT NULL,
    valor DECIMAL(10, 2),
    id_compra INT NOT NULL,
    id_produto INT NOT NULL,
    FOREIGN KEY (id_compra) REFERENCES compra(id),
    FOREIGN KEY (id_produto) REFERENCES produto(id)
);

-- Tabela de Parcelas de Compra
CREATE TABLE parcelascompra (
    id INT AUTO_INCREMENT PRIMARY KEY,
    valor DECIMAL(10, 2),
    data_pagamento DATE,
    data_vencimento DATE,
    id_compra INT NOT NULL,
    FOREIGN KEY (id_compra) REFERENCES compra(id)
);

-- Tabela de Vendas
CREATE TABLE venda (
    id INT AUTO_INCREMENT PRIMARY KEY,
    data DATETIME NOT NULL,
    nfiscal INT,
    total DECIMAL(10, 2),
    parcelas INT,
    status VARCHAR(100),
    id_cliente INT,
    id_tipo_pagamento INT,
    avista INT,
    FOREIGN KEY (id_cliente) REFERENCES cliente(id),
    FOREIGN KEY (id_tipo_pagamento) REFERENCES tipopagamento(id)
);

-- Tabela de Itens de Venda
CREATE TABLE itensvenda (
    id INT AUTO_INCREMENT PRIMARY KEY,
    quantidade FLOAT NOT NULL,
    valor DECIMAL(10, 2),
    id_venda INT NOT NULL,
    id_produto INT NOT NULL,
    FOREIGN KEY (id_venda) REFERENCES venda(id),
    FOREIGN KEY (id_produto) REFERENCES produto(id)
);

-- Tabela de Parcelas de Venda
CREATE TABLE parcelasvenda (
    id INT AUTO_INCREMENT PRIMARY KEY,
    valor DECIMAL(10, 2),
    data_pagamento DATE,
    data_vencimento DATE,
    id_venda INT NOT NULL,
    FOREIGN KEY (id_venda) REFERENCES venda(id)
);

-- Tabela de Histórico de Preços
CREATE TABLE historico_precos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_produto INT NOT NULL,
    id_fornecedor INT NOT NULL,
    preco DECIMAL(10, 2) NOT NULL,
    data_criacao DATE NOT NULL,
    FOREIGN KEY (id_produto) REFERENCES produto(id),
    FOREIGN KEY (id_fornecedor) REFERENCES fornecedor(id)
);

-- Tabela de Avaliações de Produtos
CREATE TABLE avaliacao (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_produto INT NOT NULL,
    id_funcionario INT NOT NULL,
    classificacao INT NOT NULL CHECK (classificacao BETWEEN 1 AND 5),
    comentario TEXT,
    data_avaliacao DATE NOT NULL,
    FOREIGN KEY (id_produto) REFERENCES produto(id),
    FOREIGN KEY (id_funcionario) REFERENCES funcionario(id)
);

-- Tabela de Acessos dos Usuários
CREATE TABLE acesso (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_usuario INT NOT NULL,
    data_hora_login DATETIME NOT NULL,
    endereco_ip VARCHAR(45) NOT NULL,
    FOREIGN KEY (id_usuario) REFERENCES usuario(id)
);

-- Tabela de Avaliações de Serviço
CREATE TABLE avaliacao_servico (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_funcionario INT NOT NULL,
    id_fornecedor INT NOT NULL,
    classificacao INT NOT NULL CHECK (classificacao BETWEEN 1 AND 5),
    comentario TEXT,
    data_avaliacao DATE NOT NULL,
    FOREIGN KEY (id_funcionario) REFERENCES funcionario(id),
    FOREIGN KEY (id_fornecedor) REFERENCES fornecedor(id)
);
