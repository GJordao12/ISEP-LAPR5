import { render, screen } from '@testing-library/react';
import TagsCloudPropio from '../src/Tags/TagsFromRelacoesPropio';

test('Tag Cloud Relations', () => {
    render(<TagsCloudPropio />);
    const linkElement = screen.getByText(/Tag Cloud\(Relations\):/i);
    expect(linkElement).toBeInTheDocument();
});

test('Tag Names', () => {
    render(<TagsCloudPropio />);
    const linkElement = screen.getByText(/Tag Names/i);
    expect(linkElement).toBeInTheDocument();
});