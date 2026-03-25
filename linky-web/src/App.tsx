import { useState } from 'react'
import { Link2, Copy, Check, Clock, Globe } from 'lucide-react'
import axios from 'axios'

const API_BASE_URL = 'http://localhost:5120' // Adjust if your backend port differs

function App() {
  const [url, setUrl] = useState('')
  const [expiration, setExpiration] = useState('7')
  const [isLoading, setIsLoading] = useState(false)
  const [shortUrl, setShortUrl] = useState('')
  const [copied, setCopied] = useState(false)
  const [error, setError] = useState('')

  const handleShorten = async (e: React.FormEvent) => {
    e.preventDefault()
    if (!url) return

    setIsLoading(true)
    setError('')
    setShortUrl('')

    try {
      const response = await axios.post(`${API_BASE_URL}/api/urls`, {
        originalUrl: url,
        expirationDays: parseInt(expiration, 10)
      })

      setShortUrl(response.data.shortUrl)
    } catch (err) {
      setError('Failed to shorten the URL. Please ensure the backend is running.')
      console.error(err)
    } finally {
      setIsLoading(false)
    }
  }

  const copyToClipboard = () => {
    if (!shortUrl) return
    navigator.clipboard.writeText(shortUrl)
    setCopied(true)
    setTimeout(() => setCopied(false), 2000)
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', padding: '4rem 2rem' }}>
      
      {/* Header */}
      <div style={{ textAlign: 'center', marginBottom: '3rem' }}>
        <div style={{ display: 'inline-flex', padding: '12px', background: 'var(--bg-surface)', borderRadius: 'var(--radius-xl)', marginBottom: '1rem', border: '1px solid var(--border-light)' }}>
          <Link2 size={32} color="var(--primary)" />
        </div>
        <h1 style={{ fontSize: '3rem', fontWeight: 700, letterSpacing: '-0.02em', marginBottom: '0.5rem' }}>
          Welcome to <span className="text-gradient">Linky</span>
        </h1>
        <p style={{ color: 'var(--text-secondary)', fontSize: '1.125rem', maxWidth: '500px' }}>
          A sleek, lightning-fast URL shortener with customizable expiration dates. Your beautiful links start here.
        </p>
      </div>

      {/* Main Panel */}
      <div className="glass-panel" style={{ width: '100%', maxWidth: '560px' }}>
        <form onSubmit={handleShorten}>
          
          <div className="input-group">
            <label className="input-label" style={{ display: 'flex', alignItems: 'center', gap: '6px' }}>
              <Globe size={16} /> Destination URL
            </label>
            <input 
              type="url" 
              className="input-field" 
              placeholder="https://example.com/very-long-url-path..." 
              value={url}
              onChange={(e) => setUrl(e.target.value)}
              required
            />
          </div>

          <div className="input-group">
            <label className="input-label" style={{ display: 'flex', alignItems: 'center', gap: '6px' }}>
              <Clock size={16} /> Link Availability
            </label>
            <select 
              className="input-field" 
              value={expiration} 
              onChange={(e) => setExpiration(e.target.value)}
            >
              <option value="1">1 Day</option>
              <option value="3">3 Days</option>
              <option value="7">7 Days</option>
              <option value="30">30 Days</option>
              <option value="0">Never Expires</option>
            </select>
          </div>

          <button 
            type="submit" 
            className="btn btn-primary" 
            style={{ width: '100%', marginTop: '1rem', padding: '14px' }}
            disabled={isLoading}
          >
            {isLoading ? 'Shortening Magic...' : 'Generate Short Link'}
          </button>
          
          {error && <p style={{ color: 'var(--error)', marginTop: '1rem', fontSize: '0.875rem', textAlign: 'center' }}>{error}</p>}

        </form>

        {/* Result Area */}
        {shortUrl && (
          <div className="animate-fade-in-up" style={{ marginTop: '2rem', paddingTop: '1.5rem', borderTop: '1px solid var(--border-light)' }}>
            <p className="input-label" style={{ marginBottom: '8px' }}>Your Short Link</p>
            <div style={{ display: 'flex', gap: '12px' }}>
              <input 
                type="text" 
                className="input-field" 
                value={shortUrl} 
                readOnly 
                style={{ flex: 1, backgroundColor: 'rgba(99, 102, 241, 0.1)', color: 'var(--primary)', borderColor: 'var(--border-focus)' }}
              />
              <button 
                type="button" 
                className={`btn ${copied ? 'btn-primary' : 'btn-secondary'}`}
                onClick={copyToClipboard}
                title="Copy to clipboard"
                style={{ padding: '0 16px' }}
              >
                {copied ? <Check size={20} /> : <Copy size={20} />}
              </button>
            </div>
          </div>
        )}

      </div>

      {/* Footer */}
      <div style={{ marginTop: 'auto', paddingTop: '4rem', color: 'var(--text-muted)', fontSize: '0.875rem' }}>
        Powered by React, Vite, and .NET Core.
      </div>
    </div>
  )
}

export default App
