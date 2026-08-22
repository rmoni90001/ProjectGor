USE GordonDB;
GO

-- Insert sample customers
IF NOT EXISTS (SELECT * FROM Customers WHERE Email = 'john@example.com')
BEGIN
    INSERT INTO Customers (Name, Email, RiskLevel) VALUES 
    ('John Doe', 'john@example.com', 'Normal'),
    ('Jane Smith', 'jane@example.com', 'Normal'),
    ('Bob Wilson', 'bob@example.com', 'High');
END
GO

-- Insert sample tickets
IF NOT EXISTS (SELECT * FROM Tickets WHERE Title = 'Sample Ticket 1')
BEGIN
    INSERT INTO Tickets (Title, Content, Status, Severity) VALUES
    ('Sample Ticket 1', 'User reports login issue', 'Open', 'High'),
    ('Sample Ticket 2', 'Password reset request', 'Open', 'Normal'),
    ('Sample Ticket 3', 'Feature request for dashboard', 'Closed', 'Low');
END
GO

PRINT 'Database seeding complete';
GO
