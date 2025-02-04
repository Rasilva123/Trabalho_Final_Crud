-- Criação do banco de dados
CREATE DATABASE IF NOT EXISTS CadastroEstoque;

-- Seleciona o banco de dados
USE CadastroEstoque;

-- Criação da tabela Marca
CREATE TABLE IF NOT EXISTS Marca (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
);

-- Criação da tabela Itens
CREATE TABLE IF NOT EXISTS Itens (
    id INT AUTO_INCREMENT PRIMARY KEY,
    modelo VARCHAR(255) NOT NULL,
    quantidade INT NOT NULL,
    tipo VARCHAR(100) NOT NULL,
    data_vencimento DATE,
    data_entrada DATE NOT NULL,
    nome_fornecedor VARCHAR(255) NOT NULL,
    valor_compra DECIMAL(10, 2) NOT NULL,
    valor_venda DECIMAL(10, 2) NOT NULL,
    marca_id INT,
    FOREIGN KEY (marca_id) REFERENCES Marca(id) ON DELETE SET NULL
);

-- Inserindo marcas
INSERT INTO Marca (nome) VALUES ('Marca A');
INSERT INTO Marca (nome) VALUES ('Marca B');
INSERT INTO Marca (nome) VALUES ('Marca C');

-- Inserindo itens
INSERT INTO Itens (modelo, quantidade, tipo, data_vencimento, data_entrada, nome_fornecedor, valor_compra, valor_venda, marca_id) 
VALUES ('Item 1', 10, 'Tipo A', '2023-12-31', CURDATE(), 'Fornecedor A', 100.00, 150.00, 1);

INSERT INTO Itens (modelo, quantidade, tipo, data_vencimento, data_entrada, nome_fornecedor, valor_compra, valor_venda, marca_id) 
VALUES ('Item 2', 5, 'Tipo B', '2023-11-30', CURDATE(), 'Fornecedor B', 50.00, 75.00, 2);

INSERT INTO Itens (modelo, quantidade, tipo, data_vencimento, data_entrada, nome_fornecedor, valor_compra, valor_venda, marca_id) 
VALUES ('Item 3', 20, 'Tipo C', '2024-01-15', CURDATE(), 'Fornecedor C', 200.00, 300.00, 3);