import { render, screen } from '@testing-library/react';
import Groups from '../src/Groups/ShowGroups';

test('Minimum Number of Elements', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Minimum Number of Elements:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Amount of Tags in common', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Amount of Tags in common:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Tags', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Tags:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Minimum Number of Elements', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Minimum Number of Elements:/i);
    expect(linkElement).toBeInTheDocument();
});

test('Suggest Group', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Suggest Group/i);
    expect(linkElement).toBeInTheDocument();
});

test('Clear Input', () => {
    render(<Groups />);
    const linkElement = screen.getByText(/Clear Input/i);
    expect(linkElement).toBeInTheDocument();
});