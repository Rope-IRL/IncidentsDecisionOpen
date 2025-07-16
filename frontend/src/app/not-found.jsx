'use client'
import "./globals.css"

function NotFound() {
  return (
    <div className = "container_take_free_space take_all_free_space error_wrapper">
        <div className = "error_description">
          <div className="error_code">
            404
          </div>
          <div className="error_meaning">
            |
          </div>
          <div className = "error_meaning">
            Not Found
          </div>
        </div>
    </div>
  )
}
export default NotFound