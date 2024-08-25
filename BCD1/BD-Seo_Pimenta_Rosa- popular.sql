USE `seopimenta`;

-- Insert into endereco
INSERT INTO `endereco` (`logradouro`, `numero`, `bairro`, `cidade`, `estado`, `cep`) VALUES
('Rua das Flores', '123', 'Jardim das Rosas', 'São Paulo', 'SP', '01234567'),
('Avenida Paulista', '1500', 'Bela Vista', 'São Paulo', 'SP', '01310000');

-- Insert into telefone
INSERT INTO `telefone` (`numero`) VALUES
('11999999999'),
('11888888888');

-- Insert into cargo
INSERT INTO `cargo` (`nome`, `data_inicio`, `data_fim`) VALUES
('Gerente', '2020-01-01', NULL),
('Assistente', '2021-06-15', NULL);

INSERT INTO `seopimenta`.`funcionario` (`id`, `nome`, `cpf`, `email`, `senha`, `id_endereco`, `id_telefone`, `id_cargo`) VALUES
(1, 'João Silva', '123.456.789-00', 'joao.silva@example.com', 'senha123', 1, 1, 1),
(2, 'Maria Oliveira', '987.654.321-00', 'maria.oliveira@example.com', 'senha456', 2, 2, 2);

INSERT INTO `seopimenta`.`usuario` (`nivel`, `usuario`, `senha`, `id_funcionario`, `disponibilidade`, `data_criacao`) VALUES
(2, 'joaoadmin', SHA2('senha123', 256), 1, 1, NOW()),
(1, 'mariauser', SHA2('senha123', 256), 2, 1, NOW());

-- Insert into usuario
-- INSERT INTO `usuario` (`nivel`, `usuario`, `senha`, `id_funcionario`, `disponibilidade`, `data_criacao`) VALUES
-- (2, 'joaoadmin', SHA256('senha123', 256), 1, 1, NOW()),
-- (1, 'mariauser', SHA256('senha123', 256), 2, 1, NOW());

-- Insert into acesso
INSERT INTO `acesso` (`data_hora_login`, `endereco_ip`, `id_usuario`) VALUES
('2024-08-01 10:00:00', '192.168.0.1', 1),
('2024-08-01 11:00:00', '192.168.0.2', 2);

-- Insert into categoria
INSERT INTO `categoria` (`nome`) VALUES
('Eletrônicos'),
('Móveis');

-- Insert into fornecedor
INSERT INTO `fornecedor` (`nome_fantasia`, `razao_social`, `cnpj`, `id_endereco`, `id_telefone`) VALUES
('Fornecedor A', 'Fornecedor A Ltda', '12.345.678/0001-99', 1, 1),
('Fornecedor B', 'Fornecedor B Ltda', '98.765.432/0001-11', 2, 2);

-- Insert into undmedida
INSERT INTO `undmedida` (`nome`) VALUES
('Unidade'),
('ml'),
('Kg'),
('g');

-- Insert into produto
-- INSERT INTO `produto` (`nome`, `descricao`, `imagem`, `valor_compra`, `valor_venda`, `id_unidade_medida`, `id_categoria`, `id_subcategoria`) VALUES
-- ('Notebook', 'Notebook de última geração', 'notebook.jpg', 2500.00, 3000.00, 1, 1, NULL),
-- ('Mesa', 'Mesa de madeira', 'mesa.jpg', 200.00, 350.00, 2, 2, NULL);

-- Insert into compra
INSERT INTO `compra` (`data_`, `nfiscal`, `total`, `parcelas`, `status_`, `id_fornecedor`, `id_tipo_pagamento`) VALUES
('2024-07-20 14:00:00', 123456, 3000.00, 3, 'Pago', 1, NULL),
('2024-07-25 10:30:00', 654321, 700.00, 2, 'Pendente', 2, NULL);

-- Insert into itenscompra
-- INSERT INTO `itenscompra` (`quantidade`, `valor`, `id_compra`, `id_produto`) VALUES
-- (1, 2500.00, 1, 1),
-- (2, 350.00, 2, 2);

-- Insert into consumo
-- INSERT INTO `consumo` (`data_`, `nfiscal`, `total`, `parcelas`, `avista`, `status_`, `id_compra`, `id_produto`) VALUES
-- ('2024-08-01 15:00:00', NULL, 3500.00, 1, 1, 'Concluído', 1, 1);

-- Insert into itensconsumo
-- INSERT INTO `itensconsumo` (`quantidade`, `valor`, `id_consumo`, `id_produto`) VALUES
-- (1, 3000.00, 1, 1),
-- (1, 500.00, 1, 2);

-- Insert into estoque
-- INSERT INTO `estoque` (`quantidade`, `id_produto`) VALUES
-- (10, 1),
-- (15, 2);

-- Insert into historico_precos
-- INSERT INTO `historico_precos` (`preco`, `data_criacao`, `id_produto`, `id_fornecedor`) VALUES
-- (2500.00, '2024-06-01', 1, 1),
-- (200.00, '2024-06-15', 2, 2);

-- Insert into salario
INSERT INTO `salario` (`salario`, `data_inicio`, `data_fim`, `id_funcionario`) VALUES
(5000.00, '2024-01-01', NULL, 1),
(3000.00, '2024-06-01', NULL, 2);